using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateMilkPickup : MonoBehaviour
{

    [SerializeField] Animator animator;
   
    private float timer = 0f;
    private float timeBeforeReset = 3f;

    private bool isSlowMotion;

    private string pickup = "Pickup";


    // Update is called once per frame
    void Update()
    {
        if (timer >= timeBeforeReset)
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            isSlowMotion = false;
        }
        if (isSlowMotion) { 
            timer += Time.deltaTime; 
        }
        
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { 
            isSlowMotion = true;
            Time.timeScale = 0.2f;
            animator.SetBool(pickup, true);
        }
    }
}
