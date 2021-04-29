using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkPickup : MonoBehaviour {
	[SerializeField] private Collider myCollider = null;
	[SerializeField] [Range(.75f, 2f)] private float sizeMultiplier = 2f;
	[SerializeField] Animator animator;
	private float groundHeight = -.5f;
	private string player = "Player";
	private string pickup = "Pickup";
	private SoundController soundController;
	//[SerializeField] private string pickupMilkString = "Drink";

	private void Start() {
		soundController = SoundController.onlySoundController;
	}



	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals(player)) {

			myCollider.enabled = false;
			Transform cow = other.gameObject.transform;
			StartCoroutine(Grow(cow, sizeMultiplier));
			//cow.localScale *= sizeMultiplier;
			cow.transform.position =
					new Vector3(cow.position.x,
							groundHeight + cow.localScale.y / 2f,
							cow.position.z);
			cow.GetComponent<Rigidbody>().drag = sizeMultiplier * 4f;
			transform.LookAt(other.transform);
			animator.SetBool(pickup, true);
		}
	}

	private IEnumerator Grow(Transform cow, float size) {
		Vector3 startSize = cow.localScale;
		Vector3 endSize = startSize * sizeMultiplier;
		float t = 0f;
		float e;
		while(t < 1f) {
			e = Ease.EaseOutElastic(t);
			cow.localScale = Vector3.LerpUnclamped(startSize, endSize, e);
			t += Time.deltaTime *.5f;
			yield return null;
		}
		cow.localScale = startSize * sizeMultiplier;
	}

	/*public void PlayPickupSound() {
		soundController.PlaySound(pickupMilkString);
	}*/
}