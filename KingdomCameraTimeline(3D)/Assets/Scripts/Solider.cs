using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Solider : BaseUnit
{
    private SpriteRenderer CirCle;
   

    public override  void Start()
    {
        base.Start();
       
        CirCle = transform.Find("SelectionCircle").GetComponent<SpriteRenderer>();
        SetSelected(false);
    }
   

    public void SetSelected(bool b)
    {
       
        var oldColor = CirCle.color;
        oldColor = b ? new Color(0f, 255f, 139f, 255f): Color.yellow;
        CirCle.color = oldColor;
    }
   

   
}
