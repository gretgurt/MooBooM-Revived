using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceIn : MonoBehaviour {

	[SerializeField] private float height = 10f;
	[SerializeField] private float sidewaysDistance = 4f;
	private string hittingGrass = "hittingGrass";
	private SoundController soundController;

    void Start() {
		soundController = SoundController.onlySoundController;
		StartCoroutine(Bounce());
	}

	private IEnumerator Bounce() {
		Vector3 currPos = transform.position;
		Vector3 endPos = currPos;
		float startHeight = currPos.y + height + Random.Range(-2f,2f);
		float startSide = currPos.x;
		startSide = startSide < 0f
				? startSide - sidewaysDistance
				: startSide + sidewaysDistance;

		currPos.x = startSide;
		currPos.y = startHeight;
		transform.position = currPos;

		float delay = Random.Range(.5f, 1.5f);
		yield return new WaitForSeconds(delay);

		soundController.PlaySoundWithDelay(hittingGrass, 0.2f);


		float time = 0;
		while (time < 1f) {
			float bounce = Ease.EaseOutBounce(time);
			float sideWays = Ease.EaseOutQuint(time);
			
			time += Time.deltaTime;
			currPos.x = Mathf.Lerp(startSide, endPos.x, sideWays);
			currPos.y = Mathf.Lerp(startHeight, endPos.y, bounce);
			transform.position = currPos;
			yield return null;
		}
		if (time >= 1f) {
			transform.position = endPos;
		}
	}
}