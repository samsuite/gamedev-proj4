using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ScratchRay : MonoBehaviour {
	public LayerMask layerMask;
	public bool toggledOn = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (toggledOn && !EventSystem.current.IsPointerOverGameObject ()) {
			if (Input.GetMouseButton (0)) {
				RaycastHit hit;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000f, layerMask)) {
					StencilPlane sp = hit.transform.gameObject.GetComponent<StencilPlane> ();
					if (sp != null) {
						Vector3 point = (hit.transform.InverseTransformPoint (hit.point) + new Vector3 (.5f, 0, .5f)) * sp.segments;
						int x = (int)point.z;
						int y = ((int)point.x) * sp.segments;
						sp.colors [x + y] = new Color32 (255, 255, 255, 255);
						sp.mesh.SetColors (sp.colors);
						sp.mesh.UploadMeshData (false);
					}
				}
			}
		}
	}
}
