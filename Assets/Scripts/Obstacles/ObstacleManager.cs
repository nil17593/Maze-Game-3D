using System.Collections.Generic;
using UnityEngine;

namespace RoninLabs.Maze3D
{
    /// <summary>
    /// Manager class for all the in game obstacle
    /// activates and deactivates obstacles depending on the players position
    /// </summary>
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
}
