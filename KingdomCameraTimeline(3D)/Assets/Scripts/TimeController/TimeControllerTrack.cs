using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1f, 0.8433224f, 0f)]
[TrackClipType(typeof(TimeControllerClip))]
public class TimeControllerTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
       var scriptPlayable= ScriptPlayable<TimeControllerMixerBehaviour>.Create (graph, inputCount);
        var b = scriptPlayable.GetBehaviour();
        foreach (var c in GetClips())
        {
            TimeControllerClip clip = c.asset as TimeControllerClip;

            if (clip.template.Type==MarkerType.Marker)
            {
                b.markerClips.Add(clip.template.markerToJumpTo,c.start);
            }
        }
        return scriptPlayable;
    }
}
