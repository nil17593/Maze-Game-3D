using System.Collections;
using UnityEngine;

namespace RoninLabs.Maze3D
{
    /// <summary>
    /// Trap obstacle takes array of hazrards and activates and deactivates then continuously when player is near
    /// </summary>
    public class Trap : Obstacle
    {
        public GameObject[] hazards;
        public float activationDuration = 2f;
        public float deactivationDuration = 3f;
        private Coroutine toggleCoroutine;

        void Start()
        {
            toggleCoroutine = StartCoroutine(ToggleTrap());
        }

        //coroutine for Toggle between traps active and deactivate position
        IEnumerator ToggleTrap()
        {
            if (GameManager.Instance.IsGameOver)
                yield break;

            while (true)
            {
                if (isActive)
                {
                    Activate();
                    yield return new WaitForSeconds(activationDuration);
                    Deactivate();
                    yield return new WaitForSeconds(deactivationDuration);
                }
                else
                {
                    Deactivate();
                    yield return null;
                }
            }
        }

        //override method to Activate obstacle
        public override void Activate()
        {
            foreach (GameObject hazard in hazards)
            {
                hazard.SetActive(true);
            }
        }

        public override void Deactivate()
        {
            foreach (GameObject hazard in hazards)
            {
                hazard.SetActive(false);
            }
        }
    }
}