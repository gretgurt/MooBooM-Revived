using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticleEffect : MonoBehaviour {

	[SerializeField] private float ExplosionSpeed = 2f;
	private float t;
	private float e;

	public void Explode(Vector3 pos) {
		StopAllCoroutines();
		transform.localScale = Vector3.zero;
		t = 0;
		e = 0;
		pos.y = 0.1f;
		transform.position = pos;
		StartCoroutine(Explode());
	}

	private IEnumerator Explode() {
		transform.localScale = Vector3.zero;

		while(t < 1f) {
			e = Ease.EaseOutBack(t);
			transform.localScale = Vector3.LerpUnclamped(Vector3.zero, Vector3.one, e);
			t += Time.deltaTime * ExplosionSpeed;
			yield return null;
		}
		StartCoroutine(Shrink());
	}

	private IEnumerator Shrink() {
		t = 0;
		while(t < 1f) {
			t += Time.deltaTime * 3f;
			e = Ease.EaseOutExpo(t);
			transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, e);
			yield return null;
		}
		transform.localScale = Vector3.zero;
	}
}
