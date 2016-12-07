using UnityEngine;
using System.Collections;

public class clickable_card : MonoBehaviour {

    public LayerMask layerMask;
    Renderer ren;

    void Start () {
        ren = GetComponent<Renderer>();
    }

	void Update () {

        if (GameManager.cards_left > 0){

            ren.enabled = true;
	
            if (Input.GetMouseButton (0)) {
                if (Vector3.Distance(GameManager.player.transform.position, transform.position) < 5f){
			        RaycastHit hit;
			        if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000f, layerMask)) {
                        GameManager.scratch_card(0,8);
                    }

                }
            }
        }
        else {
            ren.enabled = false;
        }

	}
}
