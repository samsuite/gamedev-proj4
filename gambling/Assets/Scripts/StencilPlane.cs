using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StencilPlane : MonoBehaviour {
	public bool changeable = true;
	public int segments = 10;
	public Mesh mesh;
	public List<Color32> colors;

	// Use this for initialization
	void Start () {
		CreatePlane();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//Changed by ScratchRay, do not edit;
	public float colorRatio = 0;
	//Called when colors are changed
	public virtual void changedColors(){

	}
	void CreatePlane(){
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
		mesh.MarkDynamic(); //mark for "frequent updates"

		//Arrays and lists
		int segmentsSqr = segments*segments;
		List<Vector3> verts = new List<Vector3>(segmentsSqr);
		List<Vector2> uvs = new List<Vector2>(segmentsSqr);
		colors = new List<Color32>(segments);
		int[] indices = new int[(segments)*(segments)*6];

		Color32 defaultColor = new Color32(0,0,0,0);
		float width = 1f / (segments-1);
		for(int i = 0; i < segments; i++){
			for(int j = 0; j< segments; j++){
				verts.Add(new Vector3(i*width-.5f,0,j*width-.5f));
				uvs.Add(new Vector2(i,j));
				colors.Add(defaultColor);

				if(i > 0 && j > 0){
					int iLast = i-1;
					int jLast = (j-1)*segments;
					int jOffset = j * segments;
					var index = (iLast+jLast)*6;
					indices[index] = iLast + jOffset;
					indices[index+1] = iLast + jLast;
					indices[index+2] = i + jOffset;

					indices[index+3] = i + jOffset;
					indices[index+4] = iLast+ jLast;     
					indices[index+5] = i + jLast;
					 
				}
			}
		}
		verts.TrimExcess();
		uvs.TrimExcess();
		colors.TrimExcess();

		mesh.SetVertices(verts);
		mesh.SetUVs(0,uvs);
		mesh.SetColors(colors);
		mesh.SetIndices(indices,MeshTopology.Triangles,0,true);
		mesh.UploadMeshData(false);
	}
	void OnDrawGizmos() {
		Gizmos.color = new Color(1f, 1f, 1f,.25f);
		RenderGizmo();
	}
	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(0, 1f, 1f,.75f);
		RenderGizmo();
	}
	void RenderGizmo(){
		Matrix4x4 rotation = Matrix4x4.TRS(transform.position,transform.rotation,transform.lossyScale);
		Matrix4x4 pushed = Gizmos.matrix;
		Gizmos.matrix = rotation;

		Gizmos.DrawCube(Vector3.zero, new Vector3(1f, 0, 1f));
		Gizmos.matrix = pushed;
	}
}
