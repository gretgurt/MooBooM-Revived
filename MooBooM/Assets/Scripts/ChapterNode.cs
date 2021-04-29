using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterNode : MonoBehaviour {

	[SerializeField] private int chapterNumber = 1;
	private Transform cow;
	private Rigidbody cowRB;
	[SerializeField][Range(1f, 50f)] private float atttractionForce = 30f;
	private float distance;
	private Vector3 direction;
	private float timer;
	[SerializeField][Range(0f, 1f)] private float timeUntilChapterSelect = .5f;
	private bool cameFromNode;
	private TransitionEffect transition;
	private bool transitioned;


	private void Start() {
		transition = FindObjectOfType<TransitionEffect>();
		cow = FindObjectOfType<Cow>().transform;
		cowRB = cow.GetComponent<Rigidbody>();
		if ((transform.position - cow.position).magnitude < .5f) {
			cameFromNode = true;
		}
		Invoke("ReleaseCow", .5f);
	}

	private void ReleaseCow() {
		cow.GetComponent<Cow>().Release();
	}

	private void FixedUpdate() {
		cameFromNode = CameFromNode();
		if (cameFromNode) {
			return;
		}

		direction = transform.position - cow.position;
		distance = (transform.position - cow.position).magnitude;
		direction = direction.normalized;

		if (distance > 1f) {
			cameFromNode = false;
			timer = 0f;
			cowRB.drag = 1f;
		} else {
			cowRB.AddForce(direction * atttractionForce);
			if (distance < .5f) {
				cowRB.drag = 1f;
				timer += Time.fixedDeltaTime;
			}
			if (timer > timeUntilChapterSelect && !transitioned) {
				transitioned = true;
				cowRB.drag = 3f;
				cowRB.useGravity = false;
				transition.Transition();
				SaveManager.SetChapterNumber(chapterNumber);
			}
		}
	}

	private bool CameFromNode() {
		if (cameFromNode) {
			if ((transform.position - cow.position).magnitude > 1f) {
				cameFromNode = false;
			}
		}
		return cameFromNode;
	}
}