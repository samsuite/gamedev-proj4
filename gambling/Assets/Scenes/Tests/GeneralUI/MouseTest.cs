using UnityEngine;
using System.Collections;

public class MouseTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void MouseEnter(){
		Debug.Log("MouseEnter");
	}
	public void MouseOver(){
		Debug.Log("MouseOver");
	}
	public void MouseExit(){
		Debug.Log("MouseExit");
	}
	public void MouseUp(){
		Debug.Log("MouseUp");
	}
	public void MouseDown(){
		Debug.Log("MouseDown");
	}
}
