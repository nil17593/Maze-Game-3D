using System.Collections;
using UnityEngine;

public class Trap : Obstacle
{
    public GameObject[] hazards;
    public float activationDuration = 2f;
    public float deactivationDuration = 3f;
    [SerializeField] private Transform player;
    private Coroutine toggleCoroutine;

    void Start()
    {
        toggleCoroutine = StartCoroutine(ToggleTrap());
    }

    IEnumerator ToggleTrap()
    {
        while (true)
        {
            bool isPlayerClose = Vector3.Distance(transform.position, player.position) < activateDistance;

            if (isPlayerClose)
            {
                isActive = !isActive;
                if (isActive)
                {
                    Activate();
                    yield return new WaitForSeconds(activationDuration);
                }
                else
                {
                    Deactivate();
                    yield return new WaitForSeconds(deactivationDuration);
                }
            }
            else
            {
                isActive = false;
                Deactivate();
            }

            yield return null;
        }
    }

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
