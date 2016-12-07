using UnityEngine;
using System.Collections;

// the player definitely needs a rigidbody
[RequireComponent (typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    public LayerMask clickLayerMask;
    public float speed;
    float turnspeed = 15f;

    Vector3 moveVector;
    float moveSpeed;

    Vector3 forwardVector;
    Vector3 rightVector;

    Rigidbody rb;

	void Start () {
        moveVector = new Vector3(0f,0f,0f);
        rb = GetComponent<Rigidbody>();
	}
	

	void Update () {

        ///// KEYBOARD CONTROL
        // we need to make sure we're moving relative to the camera angle
        rightVector = GameManager.camera.transform.right;
        forwardVector = Quaternion.Euler(0, -90, 0) * rightVector;
        moveVector = Input.GetAxis("horizontal")*rightVector + Input.GetAxis("vertical")*forwardVector;


        ///// MOUSE CONTROL
        // detects only right mouse button
        if (Input.GetMouseButton(1)){

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 9999f, clickLayerMask)){
                moveVector = hit.point - transform.position;
                moveVector.y = 0;
            }
            // make sure the ground plane is on the correct layer -- otherwise this raycast won't detect it

        }

        if (moveVector.magnitude > 0.1f){
            // add a little lerping so we don't snap directly to facing the right direction
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector), Time.deltaTime*turnspeed);
        }

        moveVector = moveVector.normalized*speed;
	}

    void FixedUpdate(){
        if (GameManager.can_walk){
            // we don't actually want to rotate using physics & we want to make sure nothing can apply torque to us
            rb.angularVelocity = Vector3.zero;

            // but we do want to MOVE using physics
            rb.AddForce(moveVector);
        }
    }
}
