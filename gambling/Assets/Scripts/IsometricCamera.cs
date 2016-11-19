using UnityEngine;
using System.Collections;

public class IsometricCamera : MonoBehaviour {

    public float pitch = 45f;
    public float yaw = 45f;
    public float distance = 10f;

    // this is a temporary value used only in lateupdate but it's stored here so we don't have to reallocate memory for it literally every frame
    Vector3 location;

    // should we track anything? (almost always true)
    bool follow = true;

    // target to follow (usually the player)
    [HideInInspector]
    public Transform target;

	void Start () {
        location = new Vector3(0f,0f,0f);
	    target = GameManager.player.transform;
	}
	
    // monobehaviors don't always call update in a reliable order, so we're using lateupdate to make sure we're pointing the camera at exactly where the player will end up after this frame
	void LateUpdate () {
	    
        if (follow){
            location.x = Mathf.Cos(yaw*(Mathf.PI/180f));
            location.z = Mathf.Sin(yaw*(Mathf.PI/180f));
            location.y = Mathf.Sin(pitch*(Mathf.PI/180f));

            location = location.normalized * distance;
            location += target.position;

            transform.position = location;
            transform.LookAt(target.position);
        }

	}
}
