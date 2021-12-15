using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class GameMgr : MonoBehaviour
{
    private PlayableDirector _director;

    public static GameMgr Instacne{ get; private set; }
    public Transform Solider;
    public Transform Monsters;

    void Start()
    {
        Instacne = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResumeTimeline();
        }
    }

    public void PauseTimeline(PlayableDirector director)
    {
        _director = director;
        _director.Pause();

    }
    public void ResumeTimeline()
    {
        if (_director !=null)
        {
            UIManager.Instance.HideDialog();
            _director.Play();
            _director = null;
        }
      

    }

    internal Transform[] GetAllSelectableUnits()
    {
        //List<Transform> results = new List<Transform>();
        //foreach (Transform rans in Solider)
        //{
        //    results.Add(rans);
        //}
        //return results.ToArray() ;
        return Solider.Cast<Transform>().ToArray();
       
    }

    public bool IsAllMonstersDead()
    {
        foreach (Transform monster in Monsters)
        {
            var unit = monster.GetComponent<BaseUnit>();
            if (!unit.IsDead)
            {
                return false;
            }
        }
        return true;
    } 
}
