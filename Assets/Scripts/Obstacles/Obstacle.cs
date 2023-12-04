using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoninLabs.Maze3D
{
    /// <summary>
    /// Base abstarct class for the Obstacles 
    /// </summary>
    public abstract class Obstacle : MonoBehaviour
    {
        public bool isActive { get; set; }
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
                player.TakeDamage();
            }
        }
    }
}