using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player1 : MonoBehaviour
{
    private NavMeshAgent agent;//导航组件
    private LineRenderer lr;//LineRenderer组件

    public Transform foodTrans;//食物位置

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        lr = GetComponent<LineRenderer>();

        //初始化LineRenderer
        InitLine();
    }

    /// <summary>
    /// 初始化LineRenderer
    /// </summary>
    private void InitLine()
    {
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.numCapVertices = 90;
        lr.numCornerVertices = 90;
        lr.material = new Material(Shader.Find("Sprites/Default"));
    }

    /// <summary>
    /// 更新LineRenderer
    /// </summary>
    private void UpdateLineRenderer()
    {
        lr.positionCount = agent.path.corners.Length;
        lr.SetPositions(agent.path.corners);
    }

    private void Update()
    {
        //设置导航位置
        agent.SetDestination(foodTrans.position);

        //更新LineRenderer
        UpdateLineRenderer();
    }
}
