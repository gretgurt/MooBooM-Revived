using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBuilder : MonoBehaviour {

	private SoundController soundController;

	private const float fenceSectionLength = 0.8f;
	private const float firstContact = 1f / 2.75f;

	private Camera cam;
	private Vector3 bottomLeftCorner;
	private Vector3 topRightCorner;
	private float screenHeight;
	private float screenWidth;
	private int numFenceSectionsOnHeight = 10;
	private int numFenceSectionsOnWidth = 5;
	[SerializeField] private GameObject fence = null;
	[SerializeField] private GameObject WallCollider = null;
	private Quaternion facingForward = Quaternion.Euler(0f, -90f, 0f);
	private Quaternion facingRight = Quaternion.Euler(0f, 0f, 0f);
	private Quaternion facingDown = Quaternion.Euler(0f, 90f, 0f);
	private Quaternion facingLeft = Quaternion.Euler(0f, 180f, 0f);
	private FenceObstacle[] fenceObstacles;
	private Vector3[] fenceObstaclePositions;

	private void Start() {
		cam = FindObjectOfType<Camera>();
		soundController = FindObjectOfType<SoundController>();
		fenceObstacles = FindObjectsOfType<FenceObstacle>();
		fenceObstaclePositions = new Vector3[fenceObstacles.Length];
		for (int i = 0; i < fenceObstacles.Length; i++) {
			fenceObstaclePositions[i] = fenceObstacles[i].transform.position;
			fenceObstacles[i].transform.position += Vector3.up * 18f;
		}
		StartCoroutine(SpawnFenceSections());
	}

	private Vector3 GetPosOfCornerFromCamera(Vector3 ViewPortCoord) {
		return Vector3.zero;
	}

	private IEnumerator SpawnFenceSections() {

		soundController.PlaySoundWithDelay("FenceSwoosh", 0.5f);
		bottomLeftCorner = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 17f));
		topRightCorner = cam.ViewportToWorldPoint(new Vector3(1f, 1f, 17f));

		screenWidth = Mathf.Abs(bottomLeftCorner.x - topRightCorner.x);
		screenHeight = Mathf.Abs(topRightCorner.z - bottomLeftCorner.z);

		Vector3 wallPos = bottomLeftCorner;
		Instantiate(WallCollider, wallPos,
					facingForward,
					this.gameObject.transform);
		wallPos += new Vector3(0f, 0f, screenHeight);
		Instantiate(WallCollider, wallPos,
					facingRight,
					this.gameObject.transform);
		wallPos += new Vector3(screenWidth, 0f, 0f);
		Instantiate(WallCollider, wallPos,
					facingDown,
					this.gameObject.transform);
		wallPos += new Vector3(0f, 0f, -screenHeight);
		Instantiate(WallCollider, wallPos,
					facingLeft,
					this.gameObject.transform);

		numFenceSectionsOnWidth = (int)(screenHeight / fenceSectionLength);
		numFenceSectionsOnHeight = (int)(screenWidth / fenceSectionLength);
		
		float sectionLengthZAxis = screenWidth / numFenceSectionsOnHeight;
		float sectionLengthXAxis = screenHeight / numFenceSectionsOnWidth;
		
		Vector3 fencePos = bottomLeftCorner + Vector3.down * .5f;
		Vector3 offset = new Vector3(0f, 0f, sectionLengthZAxis);
		for (int i = 0; i < numFenceSectionsOnWidth; i++) {
			Instantiate(fence, fencePos,
					facingForward,
					this.gameObject.transform);
			fencePos += offset;
			yield return null;
		}
		offset = new Vector3(sectionLengthXAxis, 0f, 0f);
		for (int i = 0; i < numFenceSectionsOnHeight; i++) {
			Instantiate(fence, fencePos,
					facingRight,
					this.gameObject.transform);
			fencePos += offset;
			yield return null;
		}
		offset = new Vector3(0f, 0f, -sectionLengthZAxis);
		for (int i = 0; i < numFenceSectionsOnWidth; i++) {
			Instantiate(fence, fencePos,
					facingDown,
					this.gameObject.transform);
			fencePos += offset;
			yield return null;
		}
		offset = new Vector3(-sectionLengthXAxis, 0f, 0f);
		for (int i = 0; i < numFenceSectionsOnHeight; i++) {
			Instantiate(fence, fencePos,
					facingLeft,
					this.gameObject.transform);
			fencePos += offset;
			yield return null;
		}
		for (int i = 0; i < fenceObstacles.Length; i++) {
			StartCoroutine(DropFence(fenceObstacles[i], i));
		}
		soundController.StopSoundLoop("FenceSwoosh");

	}

	private IEnumerator DropFence(FenceObstacle fence, int index) {
		float t = 0f;
		float s;
		Vector3 startPos = fence.transform.position;
		float inv;
		while (t < 1f) {
			t += Time.deltaTime;
			s = t < firstContact ? 1f : Ease.EaseOutBounce(t);
			
			inv = Mathf.InverseLerp(0f, firstContact, t);
			fence.transform.localScale = new Vector3(1f, s, 1f);
			fence.transform.position = Vector3.Lerp(startPos, fenceObstaclePositions[index], inv);
			yield return null;
		}
	}

	private void SpawnWallColliders(Vector3 startPos) {


	}
}
