using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DebugCanvas : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI curfps;
    [SerializeField] private TextMeshProUGUI avgfps;
    [SerializeField] private TextMeshProUGUI gravityX;
    [SerializeField] private TextMeshProUGUI gravityY;
    [SerializeField] private TextMeshProUGUI gravityZ;
    private List<float> milliseconds = new List<float>();
    private LevelTwist levelTwist;
    private int counter = 0;
    private float totalMilliSeconds;
    private Vector3 gyroGravity = Vector3.down;

    void Start() {
        levelTwist = FindObjectOfType<LevelTwist>();
        for (int i = 0; i < 300; i++) {
            milliseconds.Add(Time.deltaTime);
        }
    }

    
    private void Update() {
        milliseconds.Add(Time.deltaTime);
        milliseconds.Remove(milliseconds[0]);
        
        counter++;
        if (counter == 100) {
            counter = 0;
            totalMilliSeconds = 0;
            for (int i = 0; i < milliseconds.Count; i++) {
                totalMilliSeconds += milliseconds[i];
		    }
            avgfps.text = Mathf.RoundToInt(1f / (totalMilliSeconds / milliseconds.Count)).ToString();
		}
        //gyroGravity = levelTwist.GetGyroV3();
        gyroGravity = Physics.gravity;

        gyroGravity.x = TwoDecimals(gyroGravity.x);
        gyroGravity.y = TwoDecimals(gyroGravity.y);
        gyroGravity.z = TwoDecimals(gyroGravity.z);

		gravityX.text = (gyroGravity.x).ToString();
		gravityY.text = (gyroGravity.y).ToString();
		gravityZ.text = (gyroGravity.z).ToString();
	}

    private float TwoDecimals(float decimalNumber) => (float)(Math.Truncate(decimalNumber * 100) / 100);
}
