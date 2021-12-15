using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class CommandClip : PlayableAsset, ITimelineClipAsset
{
     public CommandBehaviour template = new CommandBehaviour ();
    public ExposedReference<BaseUnit> Target;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CommandBehaviour>.Create (graph, template);
        CommandBehaviour clone = playable.GetBehaviour ();
        clone.AttackTarget = Target.Resolve(graph.GetResolver());
        return playable;
    }
}
