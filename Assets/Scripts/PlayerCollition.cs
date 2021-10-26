using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollition : MonoBehaviour
{

    public PlayerMovement movement;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstical")
        {
            movement.enabled = false; 
        }
    }
}
