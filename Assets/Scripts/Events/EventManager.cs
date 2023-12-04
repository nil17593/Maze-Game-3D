using System;

namespace RoninLabs.Maze3D
{
    public class EventManager : Singleton<EventManager>
    {
        public delegate void GameOverAction(); // Define delegate for game over action
        public event GameOverAction OnGameOver; // Event triggered when the game is over

        public delegate void CheckpointReachedAction(CheckpointData data); // Define delegate for checkpoint reached action
        public event CheckpointReachedAction OnCheckpointReached; // Event triggered when checkpoint is reached

        public void TriggerGameOverEvent()
        {
            OnGameOver?.Invoke(); // Invoke the game over event
        }

        public void TriggerCheckpointReachedEvent(CheckpointData data)
        {
            OnCheckpointReached?.Invoke(data); // Invoke the checkpoint reached event with the provided data
        }

    }
}