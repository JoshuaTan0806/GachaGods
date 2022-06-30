using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    CharacterStats stats;

    CharacterStats target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        if (stats.IsDead())
            return;

        if (HasTarget() && !TargetIsInRange())
            target = null;

        if (!HasTarget())
            target = FindClosestEnemy();

        if (CanCastSpell())
            Cast();

        if (TargetIsInRange())
            Attack();
        else
            Move();
    }

    CharacterStats FindClosestEnemy()
    {
        return null;
    }

    bool HasTarget()
    {
        return target != null;
    }

    bool TargetIsInRange()
    {
        return Vector3.SqrMagnitude(transform.position - target.transform.position) < stats.GetStat(Stat.Range);
    }

    bool CanCastSpell()
    {
        return false;
    }

    void Attack()
    {

    }

    void Move()
    {
        agent.SetDestination(target.transform.position);
    }

    void Cast()
    {

    }
}