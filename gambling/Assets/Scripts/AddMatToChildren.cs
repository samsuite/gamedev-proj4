using UnityEngine;
using System.Collections;

public class AddMatToChildren : MonoBehaviour {
	public Material material;
	// Use this for initialization
	void Start () {
		MeshRenderer[] oldMats = GetComponentsInChildren<MeshRenderer> ();
		for (int i = 0; i < oldMats.Length; i++) {
			Material mat = new Material (material);
			Material oldMat = oldMats [i].material;
			mat.CopyPropertiesFromMaterial (oldMat);
			oldMats [i].material = mat;
			//mat.color = oldMat.color;
			//mat.SetColor("_EmissionoldMat.GetColor("_Emission")
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
