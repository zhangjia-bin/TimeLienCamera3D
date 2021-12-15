using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

//动画资产
public class DialogClip : PlayableAsset
{

    public DialogBehaviour Template;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogBehaviour>.Create(graph, Template);

        //var behaviour = playable.GetBehaviour();

        return playable;
    }
   // public ClipCaps clipCaps { get; }
}
