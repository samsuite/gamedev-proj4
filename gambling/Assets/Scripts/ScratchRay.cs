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
		//If not clicking ui
		if (toggledOn && !EventSystem.current.IsPointerOverGameObject ()) {
			if (Input.GetMouseButton (0)) {
				RaycastHit hit;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000f, layerMask)) {
					StencilPlane sp = hit.transform.gameObject.GetComponent<StencilPlane> ();
					if (sp != null && sp.changeable) {
						Vector3 point = (hit.transform.InverseTransformPoint (hit.point) + new Vector3 (.5f, 0, .5f)) * sp.segments;
						int x = (int)point.z;
						int y = ((int)point.x) * sp.segments;
						sp.colorRatio += (1f / (sp.colors.Count*255f)) * (255 - sp.colors [x + y].a);
						sp.colors [x + y] = new Color32 (255, 255, 255, 255);
						sp.changedColors ();
						sp.mesh.SetColors (sp.colors);
						sp.mesh.UploadMeshData (false);
					}
				}
			}
		}
	}
}
