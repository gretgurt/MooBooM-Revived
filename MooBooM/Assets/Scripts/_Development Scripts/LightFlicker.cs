using UnityEngine;
using System.Collections.Generic;

public class LightFlicker : MonoBehaviour {

    // Params
    [SerializeField] float amountIntensity = 3f;
    [SerializeField] float amountRange = 0.6f;
    [SerializeField] float amountPos = 0.3f;
    [SerializeField] float speed = 1f;

    // State
    float startIntensity;
    float startRange;
    Vector3 startPos;

    float randomSpeed;
    Vector3 targetValue = Vector3.zero;
    Vector3 currentValue = Vector3.zero;

    // Refs
    Light tLight;

    void Start() {
        tLight = GetComponent<Light>();
        startIntensity = tLight.intensity;
        startRange = tLight.range;
        startPos = transform.position;
        Randomize();
    }

    void Randomize() {
        targetValue = Random.insideUnitSphere;
        randomSpeed = Random.Range(10f, 40f) * speed;
    }

    void Update() {
        // Set next random target
        if (Vector3.Distance(currentValue, targetValue) < 0.1f) {
            Randomize();
        }
        // Interpolate change over time
        currentValue = Vector3.Lerp(currentValue, targetValue, Time.deltaTime * randomSpeed);

        // Apply values
        tLight.intensity = startIntensity + currentValue.x * amountIntensity;
        tLight.range = startRange + currentValue.y * amountRange;
        transform.position = startPos + currentValue * amountPos;
    }
}
