using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip deathSound;
    public AudioClip shieldPickUpSound;
    public AudioClip shieldHitSound;
    public AudioClip jumpSound;

    public AudioSource effects;

    public void PlayOnHit(Collision collisionInfo, bool hasShield)
    {
        if (collisionInfo.gameObject.CompareTag("ShieldPickUp"))
        {
            effects.PlayOneShot(shieldPickUpSound, 75f);
        }

        if (collisionInfo.collider.tag == "Obstical")
        {
            if (hasShield) 
            {
                effects.PlayOneShot(shieldHitSound, 75f);
            }
            else
            { //No Shield
                effects.PlayOneShot(deathSound, 75f);
            }
        }

        if (collisionInfo.gameObject.CompareTag("JumpPad"))
        {
            effects.PlayOneShot(jumpSound, 100f);
        }
    }
}
