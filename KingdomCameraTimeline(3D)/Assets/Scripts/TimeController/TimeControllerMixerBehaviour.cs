using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.IO;
using System.Collections.Generic;

public class TimeControllerMixerBehaviour : PlayableBehaviour
{

  public  Dictionary<string, double> markerClips = new Dictionary<string, double>();
    // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        int inputCount = playable.GetInputCount ();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<TimeControllerBehaviour> inputPlayable = (ScriptPlayable<TimeControllerBehaviour>)playable.GetInput(i);
            TimeControllerBehaviour input = inputPlayable.GetBehaviour ();

            // Use the above variables to process each frame of this playable.
            if (inputWeight>0)
            {
                switch (input.Type)
                {
                    case MarkerType.JumpToMarker:
                        if (input.ConditionMet())
                        {
                            var t = markerClips[input.markerToJumpTo];
                            var director = playable.GetGraph().GetResolver() as PlayableDirector;
                            if (director != null) director.time = t;

                        }
                        break;
                }
            }
        }
    }
}
