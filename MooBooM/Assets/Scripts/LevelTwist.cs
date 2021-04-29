using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTwist : MonoBehaviour {
    private Camera cam;
    [SerializeField] private Vector3 mouseStartPos = Vector3.zero;
    [SerializeField] private Vector3 gravityForce = Vector3.down * 20f;
    [SerializeField] private float gravityFactor = 9f;
    [SerializeField] private float touchHeight = -.5f;
    [SerializeField] [Range(0f, 20f)] private float gyroscopeSensitivity = 10f;

    private UnityEngine.Gyroscope gyro;
    private bool isGyroEnabled;
    private bool isGyroSettingsOn = true;
    private Quaternion gyroOffset;
    private Vector3 phoneGravity;
    private static Quaternion xAxisOnly = new Quaternion(0, 1, 0, 0);
    private float prevScreenOrientation = 0f;
    private bool calibrating;
    private float calibrationPercent;
    
    // Canvas
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    private Vector2 centerPosCanvas;

    public void SetCalibrationCanvasPosition(Vector2 offsetPos) {
        image1.rectTransform.position = centerPosCanvas + offsetPos;
    }

    private void Start() {
        cam = Camera.main;
        isGyroEnabled = EnableGyro();
        centerPosCanvas = image1.rectTransform.position;
    }

	private bool EnableGyro() {
        if (SystemInfo.supportsGyroscope) {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
		}
        return false;
	}

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            mouseStartPos = ScreenPointToWorldPosition(touchHeight);
		}
        if (Input.GetMouseButton(0)) {
            if (isGyroEnabled) {
                SetDeviceRotation();
            }
            gravityForce =
                    (ScreenPointToWorldPosition(touchHeight) - mouseStartPos)
                    * gravityFactor
                    * gravityFactor;
            gravityForce += Vector3.down * 20f;
            Physics.gravity = gravityForce;
        }
        if (Input.GetMouseButtonUp(0)) {
            calibrating = false;
        }
        else if (isGyroEnabled && isGyroSettingsOn) {
            OrientToGravity();
        }
    }

    private Vector3 ReadGyroscope() {
        Vector3 gravity = Input.gyro.gravity;
        return gyroOffset * gravity;
	}

    private void SetDeviceRotation() {
        if (!calibrating) {
            calibrating = true;
            calibrationPercent = 0f;
            prevScreenOrientation = (Input.gyro.attitude * xAxisOnly).eulerAngles.x;
        }
        calibrationPercent += Time.deltaTime;
        float xAngle = (Input.gyro.attitude * xAxisOnly).eulerAngles.x;
        float calibrationProgress = Ease.EaseInOutCirc(calibrationPercent);
        float newAngle = Mathf.Lerp(
                prevScreenOrientation,
                xAngle,
                calibrationProgress);

        Vector2 pos = new Vector2(Input.gyro.gravity.x, xAngle) * 20f;
        // Lerp scale x
        // Lerp scale y of bubble independently
        gyroOffset =
                Quaternion.Euler(-newAngle, 0f, 0f);

        // press screen
        // 
        // Lerp from currentRotation to prevRotation, 1f - time.deltaTime * calibrationSpeed


    }

    private void OrientToGravity() {
        phoneGravity = ReadGyroscope();
        phoneGravity.x *= gyroscopeSensitivity;
        phoneGravity.z = phoneGravity.y * gyroscopeSensitivity;
        phoneGravity.y = -20f;
        Physics.gravity = phoneGravity;
	}

    private Vector3 ScreenPointToWorldPosition(float y) {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.down, new Vector3(0, y, 0));
        ground.Raycast(mousePos, out float distance);
        return mousePos.GetPoint(distance);
	}

    public Vector3 GetGyroV3() {
        return phoneGravity;
	}
}