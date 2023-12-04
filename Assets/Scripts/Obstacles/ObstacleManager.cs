using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private List<Obstacle> obstacles;
    [SerializeField] private Transform player;
    private void Update()
    {

        foreach (Obstacle obstacle in obstacles)
        {
            float distance = Vector3.Distance(player.transform.position, obstacle.transform.position);
            if (distance <= obstacle.activateDistance)
            {
                obstacle.isActive = true;
            }
            else
            {
                obstacle.isActive = false;
            }

        }
    }
}
