using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour {

	// this class has a reference to either the bomb or mine script
	// when this gets called from the animator it 
	private IExplosive explosive;

	private void Start() {
		if (explosive == null) {
			explosive = GetComponent<IExplosive>();
		}
		if (explosive == null) {
			explosive = GetComponentInParent<IExplosive>();
		}
	}

	public void DestroyMe() {
		explosive.DestroyMe();
	}

	public void Explode() {
		explosive.Exploded();
	}

	public void PickMeUpBeforeYouGoGo() {
		explosive.PickMeUp();
	}

}