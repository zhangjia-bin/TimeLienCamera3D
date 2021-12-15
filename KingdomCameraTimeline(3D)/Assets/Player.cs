using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[SerializeField]
//[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(LineRenderer))]
public class Player : MonoBehaviour
{
    NavMeshAgent nav;
    LineRenderer lr;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        nav = GetComponent<NavMeshAgent>();
        InitPoint = transform.position;
        InitLine();

    }

    private void InitLine()
    {
        lr.startColor = Color.blue;
        lr.endColor = Color.green;
        lr.startWidth =0.1f;
        lr.endWidth = 0.1f;
        lr.numCapVertices = 180;
        lr.numCornerVertices = 180;
        lr.material = new Material(Shader.Find("Sprites/Default"));
    }

    Vector3 InitPoint;
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit,1000))
            {
                InitPoint = hit.point;
            }
        }
        nav.SetDestination(InitPoint);
        UpdateLineRenderer();
       
    }

    private void UpdateLineRenderer()
    {
        Vector3[] path = nav.path.corners;
        print(path.Length);
         lr.positionCount = nav.path.corners.Length;
         lr.SetPositions(path);
    }
}
