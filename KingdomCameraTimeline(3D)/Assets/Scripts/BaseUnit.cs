using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum UnitState
{
    Idle,
    GotoAndGuard,
    GotoAndAttack,
    Guarding,
    Dead,
}

public class BaseUnit :MonoBehaviour
{

    public float HP = 100;
    public float Attack=20;
    public float AttackInterval = 1;


    public float GuardRadius = 10;

    public float CurrentHP = 100;

    public NavMeshAgent nav;
    public Animator animator;
    public Transform attackTarget = null;
    public float speed = 0.75f;

    private UnitState state = UnitState.Idle;

    public bool IsDead { get {

            return CurrentHP <= 0;
        } 
    }


    public virtual void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void StartAttacking()
    {
        StartCoroutine(DealAttacking());
    }

    public IEnumerator DealAttacking()
    {
        var enmey = attackTarget.GetComponent<BaseUnit>();
        while (attackTarget != null)
        {
            animator.SetTrigger("DoAttack");
            yield return new WaitForSeconds(AttackInterval);

            enmey.DoDamage(Attack);
            if (enmey.IsDead)
            {
                attackTarget = null;
                yield break;
            }
            yield return new WaitForSeconds(AttackInterval);
            if (enmey.state==UnitState.Dead)
            {
                yield break;
            }
        }
    }

    public void Update()
    {
        float speeds = nav.velocity.magnitude * speed;
        animator.SetFloat("Speed", speeds);

        switch (state)
        {
            case UnitState.Idle:
                break;
            case UnitState.GotoAndGuard:
                if (nav.remainingDistance<1f)Guard();
                break;
            case UnitState.GotoAndAttack:
                if (attackTarget != null)
                {
                    if (nav.remainingDistance < 1f)
                    {
                        nav.isStopped = true;
                        StartAttacking();
                    }
                }
                else
                {
                    Debug.Log("回到警戒状态");
                    Guard();
                }

                break;
            case UnitState.Guarding:
                //每一帧判断是否有敌人出现在我身边
                var unit = GetNearesHostileUnit();
                if (unit == null) break;

                Debug.Log("警戒范围内出现敌人",unit.gameObject);

                MoveAndAttack(unit.transform);

                break;
        }

    }

    private BaseUnit GetNearesHostileUnit()
    {
        var hostileTag =CompareTag("Locals")?"Foreigners":"Locals";

        var hostiles = GameObject.FindGameObjectsWithTag(hostileTag);

        BaseUnit nearestUnit = null;
        var neartesDistance = float.MaxValue;

        for (int i = 0; i < hostiles.Length; i++)
        {
            var unit = hostiles[i].GetComponent<BaseUnit>();
            if (!unit) continue;
            
            var curreDistance = Vector3.Distance(transform.position,unit.transform.position);
            if (curreDistance<GuardRadius&&curreDistance<neartesDistance)
            {
                nearestUnit = unit;
                neartesDistance = curreDistance;
            }
        }

        return nearestUnit;
    }

    private void Guard()
    {
        Debug.Log("进入警戒状态");
        nav.isStopped = true;

        nav.velocity = Vector3.zero;

        state = UnitState.Guarding;
    }

    public void SetPos(Vector3 pos)
    {
        //attackTarget = null;
        nav.isStopped = false;
        nav.SetDestination(pos);
    }


    internal void DoDamage(float attack)
    {
        if (state == UnitState.Dead)
        {
            print(UnitState.Dead.ToString());
            return;
        }
        

        CurrentHP -= attack;
        if (IsDead)
        {
            state = UnitState.Dead;
            animator.SetTrigger("DoDeath");
            Destroy(gameObject,3);
            
        }
    }

    public void MoveAndAttack(Transform target)
    {
        state = UnitState.GotoAndAttack;
        if (target == null) return;
        nav.isStopped = false;
        attackTarget = target;
        SetPos(target.position);
    }

    /// <summary>
    /// 原地警戒的状态
    /// </summary>
    /// <param name="targetPosition"></param>
    internal void GotoAndGuard(Vector3 targetPosition)
    {
        Debug.Log("开始移动到警戒位置"); 
        state = UnitState.GotoAndGuard;
        print(targetPosition);
        SetPos(targetPosition);
    }
}
