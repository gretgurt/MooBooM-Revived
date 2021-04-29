using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseSegment : MonoBehaviour {

	[SerializeField] private GameObject fuseSegmentPrefab;
	private GameObject instance;

	// ScriptA's method which spawns instances of prefab B
	// GameObject bInstance = Instantiate(prefabB);
	// ScriptB scriptOnBInstance = bInstance.GetComponent<ScriptB>();

	// scriptOnBInstance.referenceToAGameObject = this.gameObject;

	public void SpawnFuseSegment(int numSegmentsLeft) {
		instance = this.gameObject;
		Debug.Log(numSegmentsLeft);
		if (numSegmentsLeft >= 0) {
			FuseSegment next =
					Instantiate(
							instance,
							this.gameObject.transform).GetComponent<FuseSegment>();
			next.GetComponent<HingeJoint>().connectedBody = GetComponent<Rigidbody>();
			next.GetComponent<HingeJoint>().anchor = Vector3.up * 2f;
		}
	}

	public void SetYScale(float y) {
		Vector3 size = transform.localScale;
		size.y = y;
		transform.localScale = size;
	}

}
