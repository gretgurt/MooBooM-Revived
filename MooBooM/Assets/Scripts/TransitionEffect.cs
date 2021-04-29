using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionEffect : MonoBehaviour {

	[SerializeField] private Image cutOut;
	[SerializeField] private Image background;
	[SerializeField] private GameObject centerPos;
	[SerializeField] private GameObject levelSelect;
	private Transform cow;
	private Camera cam;
	private Vector3 cStartPos;
	private Vector3 cStartScale;
	[SerializeField][Range(0f,3f)] private float transitionSpeed = 1f;

	private void Awake() {
		cam = Camera.main;
		cow = FindObjectOfType<Cow>().transform;
	}

	public void Transition() => StartCoroutine(TransitionOut());

	private IEnumerator TransitionOut() {
		cStartPos = cutOut.rectTransform.position;
		cStartScale = cutOut.rectTransform.sizeDelta;
		float t = 0f;
		float e;
		while(t <= 1f) {
			t += Time.deltaTime * transitionSpeed;
			Vector3 pos = cam.WorldToScreenPoint(cow.position);
			e = Ease.EaseOutBounce(t);
			cutOut.rectTransform.position = Vector2.Lerp(cStartPos, pos, e);
			cutOut.rectTransform.sizeDelta = Vector2.Lerp(cStartScale, Vector2.zero, e);
			background.rectTransform.position = centerPos.transform.position;

			yield return null;
		}
		cutOut.rectTransform.sizeDelta = Vector2.zero;

		yield return new WaitForSeconds(.2f);
		levelSelect.SetActive(true);
		StartCoroutine(TransitionIn());
	}

	private IEnumerator TransitionIn() {
		float t = 0f;
		float e;
		while (t <= 1f) {
			t += Time.deltaTime * transitionSpeed;
			Vector3 pos = cam.WorldToScreenPoint(cow.position);
			e = Ease.EaseOutQuint(t);
			cutOut.rectTransform.position = Vector2.Lerp(pos, cStartPos, e);
			cutOut.rectTransform.sizeDelta = Vector2.Lerp(Vector2.zero, cStartScale, e);
			background.rectTransform.position = centerPos.transform.position;

			yield return null;
		}
		//cutOut.rectTransform.sizeDelta = Vector2.zero;
	}
}
