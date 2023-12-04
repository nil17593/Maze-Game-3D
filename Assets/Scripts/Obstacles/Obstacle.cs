using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public bool isActive { get ; set; }
    public float activateDistance = 5f;

    // Abstract method to handle activation when player is near
    public abstract void Activate();

    // Abstract method to handle deactivation when player moves away
    public abstract void Deactivate();

    private void OnTriggerEnter(Collider other)
    {
        ThirdPersonMovement player = other.GetComponent<ThirdPersonMovement>();
        if (player != null)
        {
            //player.TakeDamage();
        }
    }
}


