using UnityEngine;
using System.Collections;

public class RemoveObjectStencilPlane : StencilPlane {
	public Collider[] collidersToRemove;
	public float removeAtRatio = .5f;
	bool disabled = false;
	public override void changedColors ()
	{
		if (!disabled && colorRatio >= removeAtRatio) {
			disabled = true;
			for (int i = 0; i < collidersToRemove.Length; i++) {
				collidersToRemove [i].enabled = false;
			}
		}
		//colliderToRemove.enabled = (colorRatio < removeAtRatio);
	}
}
