using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;

/// <summary>
/// Add and remove strings to the UI as "goals", each goal is prefaced by the bullet string.
/// </summary>
[RequireComponent (typeof (Text))]
public class GoalList : MonoBehaviour {

	public string bullet = "• ";

	private Text text;
	private IList<string> stringList;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = "";
		stringList = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/// <summary>
	/// Adds text to the list.
	/// </summary>
	/// <param name="toAdd">String to add to list.</param>
	public void AddText(string toAdd){
		stringList.Add(toAdd);
		RebuildText();
	}

	/// <summary>
	/// Removes text to the list.
	/// </summary>
	/// <returns><c>true</c>, if string toRemove was found and removed, <c>false</c> if no match was found.</returns>
	/// <param name="toRemove">String to remove from list.</param>
	public bool RemoveText(string toRemove){
		if(stringList.Remove(toRemove)){
			RebuildText();
			return true;
		}
		return false;
	}

	private void RebuildText(){
		StringBuilder builder = new StringBuilder();
		for (int i = 0, stringListCount = stringList.Count; i < stringListCount; i++) {
			builder.Append(bullet);
			builder.AppendLine(stringList[i]);
		}
		text.text = builder.ToString();
	}

}
