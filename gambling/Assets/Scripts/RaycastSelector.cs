using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastSelector : MonoBehaviour {
	
	public LayerMask layerMask;
	private GameObject lastObject;
	void Start () {
	}

	void Update () {
		
		if(!EventSystem.current.IsPointerOverGameObject()){ //If mouse not blocked by UI
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray,out hit,9999f,layerMask)){
				//Only one message will be sent a frame, so there won't be much SendMessage overhead
				GameObject hitObject = hit.collider.gameObject;
				if(lastObject != hitObject){
					PointerExit();
					hitObject.SendMessage("MouseEnter",SendMessageOptions.DontRequireReceiver);
				}
				else{
					hitObject.SendMessage("MouseOver",SendMessageOptions.DontRequireReceiver);
				}

				if(Input.GetMouseButtonDown(0)){
					hitObject.SendMessage("MouseDown",SendMessageOptions.DontRequireReceiver);
				}
				else if(Input.GetMouseButtonUp(0)){
					hitObject.SendMessage("MouseUp",SendMessageOptions.DontRequireReceiver);
				}
				lastObject = hitObject;
			}
			else{
				PointerExit();

				//Movement goes here, if movement uses LMB as well

			}
		}
		else{
			PointerExit();
		}
	}
	private void PointerExit(){
		if(lastObject != null){
			lastObject.SendMessage("MouseExit",SendMessageOptions.DontRequireReceiver);
			lastObject = null;
		}
	}
}
