using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Goto(Vector3 targetPosition)
    {
        foreach (Transform u in transform)
        {
            var unit = u.GetComponent<BaseUnit>();
            unit.SetPos(targetPosition);
        }
    } 
    public void MoveAndAttack(BaseUnit target)
    {
        if (target == null) return;
        foreach (Transform u in transform)
        {
            var unit = u.GetComponent<BaseUnit>();
            unit.MoveAndAttack(target.transform);
        }
    }

    internal bool IsaAlive()
    {
        foreach (Transform monster in transform)
        {
            var unit = monster.GetComponent<BaseUnit>();
            if (!unit.IsDead)
            {
                return false;
            }
        }
        return true;
    }

    internal void GotoAndGuard(Vector3 targetPosition)
    {
        foreach (Transform u in transform)
        {
            var unit = u.GetComponent<BaseUnit>();
            unit.GotoAndGuard(targetPosition);
        }
    }
}
