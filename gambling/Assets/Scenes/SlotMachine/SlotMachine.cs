using UnityEngine;
using System.Collections;

public class SlotMachine : MonoBehaviour {
	
	public Transform arm;
	public float armDownSpeed = 2f;
	public float armUpSpeed = 3f;
	public Vector3 targetArmRotation = new Vector3(-60f,0,90);

	private bool inUse = false;

	void Start () {
	
	}

	void Update () {
		if (!inUse && Input.GetMouseButtonDown (0)){ //If machine is currently running don't check
			RaycastHit hit; 
			if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition),out hit)){ 
				if(hit.collider.gameObject.name.Equals("Handle")){
					StartCoroutine(PullHandle());
				}
			}
		}
	}
	IEnumerator PullHandle(){
		inUse = true;
		float time = 0;
		Quaternion start = arm.transform.rotation;
		//Move arm down
		while(time <= 1f){
			yield return new WaitForEndOfFrame();
			time += Time.deltaTime * armDownSpeed;
			//For a delay after it is pulled
			arm.transform.rotation = Quaternion.Lerp(arm.transform.rotation,Quaternion.Euler(targetArmRotation),time);
			//arm.transform.rotation = Quaternion.Lerp(start,Quaternion.Euler(targetArmRotation),time);
		}
		//Call spinning


		//Move arm back up
		time = 0;
		while(time <= 1f){
			yield return new WaitForEndOfFrame();
			time += Time.deltaTime * armUpSpeed;
			arm.transform.rotation = Quaternion.Lerp(Quaternion.Euler(targetArmRotation),start,time);
		}
		inUse = false;
	}
}
