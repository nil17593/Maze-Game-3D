using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoninLabs.Maze3D
{
    public class TrapFloatingPltaform : FloatingPlatform
    {
        public GameObject hazard;
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
                    ActivateTrap();
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



        public override void Activate()
        {
            base.Activate();
        }

        private void ActivateTrap()
        {
            hazard.SetActive(true);
        }

        public override void Deactivate()
        {
            hazard.SetActive(false);
        }
    }
}
