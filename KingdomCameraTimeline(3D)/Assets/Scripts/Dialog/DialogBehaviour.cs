using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


[Serializable]
public class DialogBehaviour : PlayableBehaviour
{
    public string CharacterName;
    public string Content;
    public bool PauseAfterPlay = true;

    bool showPause = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (Application.isPlaying)
        {
            //UI显示出来
            UIManager.Instance.SetDialong(CharacterName, Content);
            showPause = PauseAfterPlay;
            UIManager.Instance.ShowPauserTips(PauseAfterPlay);
        }
       
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (Application.isPlaying)
        {
            if (showPause)
            {
                //timeline暂停
                PlayableDirector director = playable.GetGraph().GetResolver() as PlayableDirector;
                //director.Pause();
                GameMgr.Instacne.PauseTimeline(director);
            }
            else
            {
                UIManager.Instance.HideDialog();
            }
        }
        
        
    }
}
