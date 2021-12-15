using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TimeControllerClip : PlayableAsset, ITimelineClipAsset
{
    public TimeControllerBehaviour template = new TimeControllerBehaviour ();
    public ExposedReference<Group> baseGroup;
   

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TimeControllerBehaviour>.Create (graph, template);
        TimeControllerBehaviour clone = playable.GetBehaviour ();
        clone.baseGroup = baseGroup.Resolve(graph.GetResolver());
        return playable;
    }
}
