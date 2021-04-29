using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FenceObstacle : MonoBehaviour {

	[SerializeField] private Vector3 gridSize = Vector3.one;
	private Vector3 position;

	private readonly float y = -.5f;
	private float zOffset = -.080563f;
	private float xOffset = -.362548f;

	private void OnDrawGizmosSelected() {
		if (!Application.isPlaying && transform.hasChanged) {
			SnapToGrid();
		}
	}
	

	private void SnapToGrid() {
		position = new Vector3(
			Mathf.Round(transform.position.x / gridSize.x) * gridSize.x + xOffset,
			y,
			Mathf.Round(transform.position.z / gridSize.z) * gridSize.z + zOffset);

		transform.position = position;
	}
}
