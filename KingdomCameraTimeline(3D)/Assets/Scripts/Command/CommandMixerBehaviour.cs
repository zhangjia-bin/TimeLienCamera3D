using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CommandMixerBehaviour : PlayableBehaviour
{
    // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var group = playerData as Group;
        int inputCount = playable.GetInputCount ();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<CommandBehaviour> inputPlayable = (ScriptPlayable<CommandBehaviour>)playable.GetInput(i);
            CommandBehaviour input = inputPlayable.GetBehaviour ();

            // Use the above variables to process each frame of this playable.

            if (inputWeight>0)
            {
                switch (input.Type)
                {
                    case CustomCommandType.GoTo:
                        group.Goto(input.TargetPosition);
                        break;
                    case CustomCommandType.AttackTarget:
                        group.MoveAndAttack(input.AttackTarget);
                        break;
                    case CustomCommandType.GoToAndGuard:
                        group.GotoAndGuard(input.TargetPosition);
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
