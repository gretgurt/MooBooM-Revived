using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCameraFrustrumWithRotation : MonoBehaviour {
	[SerializeField] [Range(0f, .5f)] private float perspectiveTwistFactor = .25f;
	[SerializeField] [Range(0f, 15f)] private float viewClampDegrees;
	[SerializeField] private Transform cameraHolder = null;
	private Camera cam;

	private void Start() {
		cam = Camera.main;
		cameraHolder.transform.position = Vector3.up * 17f;
		cam.transform.localPosition = Vector3.zero;
		cam.orthographic = false;
		cam.fieldOfView = 42f;
		cam.usePhysicalProperties = true;
	}

	private void Update() {
		SetCameraPosition();
		TurnCamera();
	}

	private void TurnCamera() {
		cam.lensShift =
				new Vector2(
						-cameraHolder.transform.position.x / 3.5f * .5f,     // magical numbers! Don't touch!
						-cameraHolder.transform.position.z / 3.5f * .275f);  // magical numbers! Don't touch!
	}

	private void SetCameraPosition() {
		Vector3 gravityDir = -Physics.gravity * perspectiveTwistFactor;
		//gravityDir.z = gravityDir.y;
		gravityDir.y = 0f;
		gravityDir = Vector3.ClampMagnitude(gravityDir, viewClampDegrees);
		gravityDir.y = 17f;
		cameraHolder.transform.position = gravityDir;
	}
}