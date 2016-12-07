using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour {

	public Vector3 newPosition;
	public float speed = .75f;
	bool open;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
			open = !open;
		
		if (open) {
			transform.localPosition = Vector3.Slerp (transform.localPosition, newPosition, speed * Time.deltaTime);
		} else {
			transform.localPosition = Vector3.Slerp (transform.localPosition, Vector3.zero, speed * Time.deltaTime);
		}

	}
}
