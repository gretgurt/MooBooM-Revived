using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour {

	[SerializeField] private Rigidbody rb;

	private void Start() {
		Freeze();
	}

	public void Release() {
		rb.isKinematic = false;
	}

	public void Freeze() {
		rb.isKinematic = true;
	}

	public void Explosion(Vector3 otherPos) {
		Debug.Log("Boom");
		rb.AddExplosionForce(500f, otherPos, 20f, 1f);
	}
}
