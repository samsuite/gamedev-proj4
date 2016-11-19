using UnityEngine;
using System.Collections;

/// <summary>
/// Pause menu. Toggled with "Cancel" Button (esc key), blocks all mouse events on screen when it is enabled.
/// </summary>
[RequireComponent (typeof (CanvasGroup))]
public class PauseMenu : MonoBehaviour {

	private bool visible = false;
	private CanvasGroup canvasGroup;
	// Use this for initialization
	void Start () {
		canvasGroup = GetComponent<CanvasGroup>();
		SetVisibility();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel")){
			visible = !visible;
			SetVisibility();
		}
	}

	void SetVisibility(){
		Time.timeScale = visible ? 0 : 1f;
		canvasGroup.alpha = visible ? 1f : 0;
		canvasGroup.blocksRaycasts = visible;
		canvasGroup.interactable = visible;
	}
}
