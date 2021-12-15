using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0f, 1f, 0f)]
[TrackClipType(typeof(CommandClip))]
[TrackBindingType(typeof(Group))]
public class CommandTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {

        foreach (var c in GetClips())
        {
            var clip = c.asset as CommandClip;
            c.displayName = clip.template.Type.ToString();
        }
        return ScriptPlayable<CommandMixerBehaviour>.Create(graph, inputCount);
    }

}
