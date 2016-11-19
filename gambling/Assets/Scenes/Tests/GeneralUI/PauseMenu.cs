using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CanvasGroup))]
public class PauseMenu : MonoBehaviour {

	private bool enabled = false;
	private CanvasGroup canvasGroup;
	// Use this for initialization
	void Start () {
		
		canvasGroup = GetComponent<CanvasGroup>();
		SetVisibility();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel")){
			enabled = !enabled;
			SetVisibility();
		}
	}

	void SetVisibility(){
		Time.timeScale = enabled ? 0 : 1f;
		canvasGroup.alpha = enabled ? 1f : 0;
		canvasGroup.blocksRaycasts = enabled;
		canvasGroup.interactable = enabled;
	}
}
