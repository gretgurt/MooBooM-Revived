using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEvent : MonoBehaviour {

	private ExplosionParticleEffect explosion;

	private void Start() {
		explosion = FindObjectOfType<ExplosionParticleEffect>();
	}

	public void ExplosionParticleEffect() {
		explosion.Explode(transform.position);
	}
}
