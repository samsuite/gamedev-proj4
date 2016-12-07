using UnityEngine;
using System.Collections;

public class RemoveObjectStencilPlane : StencilPlane {
	public Collider colliderToRemove;
	public float removeAtRatio = .5f;
	public override void changedColors ()
	{
		colliderToRemove.enabled = (colorRatio < removeAtRatio);
	}
}
