using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

//Class for testing saving text in textfield (as a test). Goes on the Text portion of an input field
[RequireComponent (typeof (InputField))]
public class TextFieldSaveTest : MonoBehaviour {
	private string filename = "Scenes/Tests/SaveGame/textfieldTest.dat";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SaveData(){
		String path = Path.Combine(Application.dataPath,filename);
		SaveManager.SaveObject(path,new TextSaveGame(GetComponent<InputField>().text));
	}
	public void LoadData(){
		String path = Path.Combine(Application.dataPath,filename);
		TextSaveGame savegame = (TextSaveGame)SaveManager.LoadObject(path);
		GetComponent<InputField>().text = savegame.text;
		GetComponent<InputField>().textComponent.verticalOverflow = VerticalWrapMode.Truncate;
		GetComponent<InputField>().textComponent.verticalOverflow = VerticalWrapMode.Overflow;
		GetComponent<InputField>().textComponent.text = savegame.text;
	}

	[Serializable]
	class TextSaveGame{
		public string text;
		public TextSaveGame(string s) {text = s;}
	}
}
