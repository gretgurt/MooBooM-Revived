using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtGravity : MonoBehaviour {

	[SerializeField] private float maxDelta = 10f;

	private void LateUpdate() {
		LookDown();
	}

	private void LookDown() {
		transform.localRotation = Quaternion.RotateTowards(
				transform.localRotation, Quaternion.Euler(
						Physics.gravity * .5f),
						maxDelta * Time.deltaTime);
	}
}