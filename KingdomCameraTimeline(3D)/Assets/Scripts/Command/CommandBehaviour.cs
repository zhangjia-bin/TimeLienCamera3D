using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class CommandBehaviour : PlayableBehaviour
{
    public CustomCommandType Type;

    public Vector3 TargetPosition;
    [HideInInspector]
    public BaseUnit AttackTarget;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }
}
public enum CustomCommandType
{
    GoTo,
    AttackTarget,
    GoToAndGuard,
}
