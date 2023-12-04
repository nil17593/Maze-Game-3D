using System;
using UnityEngine;

public static class TriggerEventRouter
{
    public delegate void TriggerEventHandler(Action<Obstacle> callback);
    public static TriggerEventHandler eventHandler;
}
