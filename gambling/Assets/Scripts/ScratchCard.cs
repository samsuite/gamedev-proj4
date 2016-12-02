using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScratchCard : MonoBehaviour {

	public GameObject baseCardItem;
	public GameObject[] icons;

	// Use this for initialization
	void Start () {
		transform.parent = Camera.main.transform;
		GenerateCard (5);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = Vector3.Lerp (transform.position, new Vector3 (0, 5f, 0), Time.deltaTime);
		//transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
	
	}
	void GenerateCard(int cost, int reward = 0){
		int rows = 4;
		int columns = 3;
		//Create back of card
		//Middle
		int winningSymbols = 2;
		List<GameObject> winningIcons = new List<GameObject> (icons);
		List<GameObject> prefabs = new List<GameObject> (icons);
		for (int i = 0; i < winningSymbols; i++) {
			int item = (int)Random.Range (0, winningIcons.Count);
			prefabs.Add (winningIcons [item]);
			winningIcons.RemoveAt (item);
		}

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++) {
				GameObject go = (GameObject)Instantiate (baseCardItem, Vector3.zero, Quaternion.identity,gameObject.transform);
				go.name = i+" "+j;
				go.transform.localPosition = new Vector3 (i * 1f, j * 1f, 0);
				bool winner = false;
				if (winner) {
					//Text differnt, matches a icon
				} else {
				//Make icon not match
				}
			}	
		}
		//Front (add scratch effect here)
	}
}
