using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    private SoundController soundController;

    // Start is called before the first frame update
    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayExplodingSound()
    {
        soundController.PlaySound("ExplosionFx");
    }

    public void DrinkMilk()
    {
        soundController.PlaySound("Drink");
    }

    public void PickupBomb()
    {
        soundController.PlaySound("PickupBomb");
    }
}
