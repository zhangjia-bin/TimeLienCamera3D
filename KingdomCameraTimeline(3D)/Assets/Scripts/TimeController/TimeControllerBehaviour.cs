using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TimeControllerBehaviour : PlayableBehaviour
{
    public MarkerType Type;

    public Condition condition;
    public string markerToJumpTo;

    [HideInInspector]
    public Group baseGroup;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    internal bool ConditionMet()
    {
        switch (condition)
        {
            case Condition.Always:
                return true;
            case Condition.AnyMonsterAlive:
                return !GameMgr.Instacne.IsAllMonstersDead();
            case Condition.GroupAlive:
                return !baseGroup.IsaAlive();
            default:
                break;
        }
        return false;
    }
}
public enum Condition{
         Always,
         AnyMonsterAlive,
         GroupAlive,
}

public enum MarkerType
{
    Marker,
    JumpToMarker
}
