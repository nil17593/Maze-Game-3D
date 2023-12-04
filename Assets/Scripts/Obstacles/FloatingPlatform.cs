using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : Obstacle
{
    [Header("Settings")]
    [SerializeField] private float floatHeight = 1.0f; // Maximum height of the floating motion
    [SerializeField] private float floatSpeed = 1.0f; // Speed of the floating motion
    [SerializeField] private Transform player;

    #region private components
    private Vector3 initialPosition;
    #endregion

    void Start()
    {
        initialPosition = transform.position;
        EventManager.Instance.OnGameOver += HandleGameOverEvent;
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnGameOver -= HandleGameOverEvent;
    }

    private void HandleGameOverEvent()
    {
        Deactivate();
    }

    private void Update()
    {
        if (!isActive || GameManager.Instance.IsGameOver)
            return;

        Activate();
    }

    // Override method triggers when player is close to this object
    public override void Activate()
    {
        float newY = initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Override method triggers when player moves far away from this object
    public override void Deactivate()
    {
        transform.position = initialPosition;
    }
}
