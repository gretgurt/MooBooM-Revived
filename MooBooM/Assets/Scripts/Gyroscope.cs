using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gyroscope : MonoBehaviour {

    [SerializeField] private Vector3 mouseStartPos = Vector3.zero;
    [SerializeField] private Vector3 gravityForce = Vector3.down * 20f;
    [SerializeField] private float gravityFactor = 9f;
    [SerializeField] private float touchHeight = 0f;
    [SerializeField] [Range(0f, 20f)] private float gyroscopeSensitivity = 10f;
    [SerializeField] private float fadeOutTime = 2f;
    private Camera cam;

    // Gyro
    private UnityEngine.Gyroscope gyro;
    private readonly Quaternion xAxisOnly = new Quaternion(0, 1, 0, 0);
    private Quaternion gyroOffset;
    private Quaternion prevAngle;
    private Quaternion currAngle;
    private Vector3 phoneGravity;
    private bool isGyroEnabled;
    private bool calibrating;
    private bool isGyroSettingsOn = true;
    private bool calibrated;
    private Vector2 calibrationOffset;
    private float smallAngle = 5f;
    private float fadeOutControlls = 0;

    // Canvas
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private Image image1 = null;
    [SerializeField] private Image image2 = null;
    [SerializeField] private Image centerPosImage = null;
    [SerializeField] private GameObject images = null;
    private Color UiDefaultColor;
    private Vector2 centerPosCanvas;
    private Vector2 scale;
    //[SerializeField] bool imageOn = false;
    [SerializeField] private Color clear = Color.white;
    private GameController gameController; //isGameWon

    private void Awake() {
        cam = Camera.main;
        
    }

    private void Start() {
        UiDefaultColor = image2.color;
        centerPosCanvas = centerPosImage.rectTransform.position;

        isGyroEnabled = EnableGyro();
        gameController = FindObjectOfType<GameController>();
    }

    private bool EnableGyro() {
        if (SystemInfo.supportsGyroscope) {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }

        return false;
    }

    public bool IsCalibrated()
    {
        if (!SystemInfo.supportsGyroscope)
        {
            images.SetActive(false);
            calibrated = true;
            calibrating = false;
        }
        if (!calibrated && !calibrating)
        {
            calibrating = true;
            StartCoroutine(Calibrate());

            return false;
        }
        return calibrated;
    }

    private IEnumerator Calibrate() {

        //calibrated = offsetPos.magnitude < smallAngle;
        images.SetActive(true);
        bool centered = false;
        float t = fadeOutTime;
        float e = t;
        float inv = t;
        Color currUiColor = UiDefaultColor;
        while (!calibrated) {
            SetDeviceRotation();

            centered = calibrationOffset.magnitude < smallAngle;

            //Debug.Log(calibrationOffset.magnitude);

            if (centered) {
                t -= Time.deltaTime;
            } else {
                t = fadeOutTime;
            }

            image1.color = Color.Lerp(clear, UiDefaultColor, t * 2f);
            image2.color = Color.Lerp(clear, UiDefaultColor, t * 2f);
            inv = Mathf.InverseLerp(0f, .1f, image1.color.a);
            centerPosImage.color = Color.Lerp(clear, UiDefaultColor, inv);

            calibrated =
                    centered
                    && t < -.5f;

            yield return null;
        }
        images.SetActive(false);
        image1.color = UiDefaultColor;
        image2.color = UiDefaultColor;
        calibrating = false;
    }

    private void Update() {
        if (gameController != null && gameController.isGameOver()) {
            LevelOutGravity();
            return;
		}
        if (Input.GetMouseButtonDown(0)) {
            mouseStartPos = ScreenPointToWorldPosition(touchHeight);
        }
        if (Input.GetMouseButton(0)) {
            TouchControllGravity();
        } else if (isGyroEnabled && isGyroSettingsOn) {
            OrientToGravity();
        }
    }

    private void LevelOutGravity() {
        Vector3 gravity = Vector3.Lerp(
                    gravityForce,//gyroOffset * Input.gyro.gravity,
                    Vector2.down * 20,
                    fadeOutControlls);
        fadeOutControlls += Time.deltaTime;

        Physics.gravity = gravity;
    }

    private void TouchControllGravity() {
        gravityForce =
                    (ScreenPointToWorldPosition(touchHeight) - mouseStartPos)
                    * gravityFactor
                    * gravityFactor;
        gravityForce += Vector3.down * 20f;
        Physics.gravity = gravityForce;
    }

    private Vector3 ScreenPointToWorldPosition(float y) {

        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.down, new Vector3(0, y, 0));
        ground.Raycast(mousePos, out float distance);
        return mousePos.GetPoint(distance);
    }

    private void OrientToGravity() {

        phoneGravity = ReadGyroscope();
        phoneGravity.x *= gyroscopeSensitivity;
        phoneGravity.z = phoneGravity.y * gyroscopeSensitivity;
        phoneGravity.y = -20f;
        Physics.gravity = phoneGravity;
    }

    private Vector3 ReadGyroscope() {

        Vector3 gravity = Input.gyro.gravity;
        return gyroOffset * gravity;
    }

    private void SetDeviceRotation() {

        if (!calibrating) {
            calibrating = true;
        }
        currAngle = (Input.gyro.attitude * xAxisOnly).normalized;
        prevAngle = Quaternion.Slerp(prevAngle, currAngle, Time.deltaTime * 2f);

        float xAngle =
                Mathf.Clamp((prevAngle * xAxisOnly).normalized.eulerAngles.x,
                0f,
                Mathf.Infinity);

        gyroOffset =
                Quaternion.Euler(xAngle, 0f, 0f).normalized;

        float angle = WrapAngle(prevAngle.eulerAngles.x)
                - WrapAngle(currAngle.eulerAngles.x);

        Vector2 pos =
                new Vector2(
                        Input.gyro.gravity.x * 200f,
                        angle);

        SetCalibrationCanvasPosition(pos);
    }

    private static float WrapAngle(float angle) {
        angle %= 360f;
        return angle > 180f ? angle - 360f : angle;
    }

    public void SetCalibrationCanvasPosition(Vector2 offsetPos) {
        image1.rectTransform.position = centerPosCanvas + offsetPos;
        image2.rectTransform.position = centerPosCanvas + offsetPos;
        float x = Mathf.InverseLerp(400f, 0f, Mathf.Abs(offsetPos.x));
        float y = Mathf.InverseLerp(400f, 0f, Mathf.Abs(offsetPos.y));

        scale.x = Mathf.LerpUnclamped(0f, 1f, x);
        scale.y = Mathf.LerpUnclamped(0f, 1f, y);
        image1.rectTransform.localScale = scale;
        image2.rectTransform.localScale = scale;
        calibrationOffset = offsetPos;
    }

    public Vector3 GetGyroV3() {
        return phoneGravity;
    }
}