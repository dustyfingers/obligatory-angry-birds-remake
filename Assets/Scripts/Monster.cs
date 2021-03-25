using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Sprite deadSprite;
    [SerializeField] ParticleSystem particleSystem;

    bool hasDied = false;

    void OnCollisionEnter2D (Collision2D collision) 
    {
        if (ShouldDieFromCollision(collision))
            Die();
    }

    
    bool ShouldDieFromCollision (Collision2D collision)
    {
        if (hasDied)
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird> ();

        if (bird != null)
            return true;

        // CHECK THE ANGLE THAT THE COLLISION HAPPENED AT
        // and make sure it it coming in from above using the normals y value
        if (collision.contacts[0].normal.y < -0.5)
            return true;

        return false;
    }
    
    void Die ()
    {
        hasDied = true;
        GetComponent<SpriteRenderer> ().sprite = deadSprite;
        particleSystem.Play ();
    }
}
