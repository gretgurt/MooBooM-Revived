using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, IExplosive
{

    private GameController gameController;
    private Animator animator;
    private SoundController soundController;
    private Cow cow;

    private string explosion = "explosion";

    private bool isExploding;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        animator = GetComponentInChildren<Animator>();
        soundController = FindObjectOfType<SoundController>();
        cow = FindObjectOfType<Cow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isExploding)
        {
            isExploding = true;
            soundController.PlaySound("ExplosionFx");
            AnimateExplosion();
            gameController.MineExploded();
            HurtCow();
        }
    }

    private void HurtCow()
    {
        Debug.Log("OUCHIEEE");

        cow.Explosion(transform.position);
        soundController.PlaySound("HurtCow");
        gameController.GameOver();
    }

    public void Exploded()
    {
        gameController.MineExploded();
    }

    public void PickMeUp()
    {
        //Kan implementeras senare som ny funktion
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void AnimateExplosion()
    {
        animator.SetBool(explosion, true);
    }
}
