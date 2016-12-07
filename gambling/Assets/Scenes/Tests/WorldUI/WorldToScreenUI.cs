using UnityEngine;

public class WorldToScreenUI : MonoBehaviour {
	
	public Transform followTransform;
	public Vector3 offset;
	public bool clampToScreen = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float z = transform.position.z;
		Vector3 vec = Camera.main.WorldToScreenPoint(followTransform.position + offset);
		transform.position = Vector3.Scale(vec,new Vector3(1f,1f,0)) + new Vector3(0,0,z);

		//For unexpected behavior involving objects being behind the camera, still not technically correct.
		if(vec.z < 0){
			transform.position = Vector3.Scale(transform.position, new Vector3(-1f,-1f,1f));
		}

		if(clampToScreen){
			RectTransform rt = (RectTransform) transform;
			Vector2 size = rt.sizeDelta;
			float x = Mathf.Clamp(transform.position.x,size.x * rt.pivot.x,Screen.width - size.x * (1-rt.pivot.x));
			float y = Mathf.Clamp(transform.position.y,size.y * rt.pivot.y,Screen.height - size.y * (1-rt.pivot.y));
			transform.position = new Vector3(x,y,transform.position.z);
		}
	}
}
