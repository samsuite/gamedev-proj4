using UnityEngine;
using System.Collections;

public class clickable_card : MonoBehaviour {

    public LayerMask layerMask;

	void Update () {
	
        if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000f, layerMask)) {
                GameManager.scratch_card(0,8);
            }
        }

	}
}
