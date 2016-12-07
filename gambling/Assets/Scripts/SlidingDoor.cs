using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour {

	public Vector3 newPosition;
	private Vector3 startPos;
	public float speed = .75f;
	bool open;
	// Use this for initialization
	void Start () {
		startPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (open) {
			transform.position = Vector3.Slerp (transform.position, startPos+ newPosition, speed * Time.deltaTime);
		} else {
			transform.position = Vector3.Slerp (transform.position, startPos, speed * Time.deltaTime);
		}
		open = false;

	}
	void OnTriggerStay(Collider c){
		open = true;
	}	
}
