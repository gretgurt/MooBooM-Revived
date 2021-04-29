using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour {

	[SerializeField] private GameObject FusePrefab;
	[SerializeField] private bool fuseRoot;
	private List<Fuse> fuseSegments = new List<Fuse>();
	private int numSegments;
	private Bomb bomb;
	private float fuseLength;
	private float fuseBurnt;
	private int currentSegment;
	private Vector3 scale;

	private void Start() {
		scale = transform.localScale;
		if (fuseRoot) {
			InstantiateFuseSegments();
		}
	}

	private void Update() {
		if (fuseRoot) {
			BurnFuse();
		}
	}

	private void InstantiateFuseSegments() {
		bomb = GetComponentInParent<Bomb>();

		fuseLength = bomb.GetTimeBeforeExploding();
		numSegments = Mathf.RoundToInt(fuseLength);

		fuseSegments.Add(this);

		for (int i = 0; i < numSegments; i++) {
			fuseSegments.Add(
					Instantiate(
							FusePrefab,
							fuseSegments[fuseSegments.Count -1].transform).GetComponent<Fuse>());
		}
	}

	private void BurnFuse() {
		fuseBurnt = fuseLength - bomb.GetTimeUntilExploding();
		currentSegment = Mathf.Clamp(Mathf.FloorToInt(fuseBurnt), 0, int.MaxValue);
		
		fuseSegments[currentSegment].SetFuseLength(fuseBurnt - currentSegment);
		if (currentSegment < fuseSegments.Count -1) {
			fuseSegments[currentSegment +1].SetFuseLength(0);
		}
	}

	public void SetFuseLength(float yScale) {
		scale.y = Mathf.Clamp01(yScale);
		this.transform.localScale
			= scale;
	}

}
