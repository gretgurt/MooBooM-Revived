using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingGyroButton : MonoBehaviour {

	[SerializeField] private GameObject gyroText;
	[SerializeField] private GameObject touchText;

	// save in playerprefs
	// have gyro read off preferences

	private void Start() {
		if (!HasGyroSupport()) {
			TouchOn(false);
		}
	}

	public void Toggle() {
		if (HasGyroSupport()) {
			TouchOn();
		}
	}

	private void TouchOn() {
		gyroText.SetActive(!gyroText.activeInHierarchy);
		touchText.SetActive(!touchText.activeInHierarchy);

		Input.gyro.enabled = gyroText.activeInHierarchy;
	}

	private void TouchOn(bool touchOn) {
		Debug.Log(touchOn);
		touchText.SetActive(!touchOn);
		gyroText.SetActive(touchOn);
	}

	private bool HasGyroSupport() => SystemInfo.supportsGyroscope;
}
