using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    CharacterStats stats;
    Animator animator;

    CharacterStats target;

    bool canChooseAction;

    public System.Action OnAttack;
    public System.Action OnSpellCast;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (stats.IsDead())
            return;

        if (!canChooseAction)
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
        OnAttack?.Invoke();
        animator.Play("Attack");
        canChooseAction = false;
        StartCoroutine(AllowAction(1 / stats.GetStat(Stat.AtkSpd)));

        Instantiate(stats.Attack.Prefab, transform.position, Quaternion.identity);
    }

    void Move()
    {
        animator.Play("Move");
        agent.SetDestination(target.transform.position);
    }

    void Cast()
    {
        OnSpellCast?.Invoke();
        animator.Play("Cast");
        canChooseAction = false;
        StartCoroutine(AllowAction(1 / stats.GetStat(Stat.SpellSpd)));

        Instantiate(stats.Spell.Prefab, transform.position, Quaternion.identity);
    }

    void Stun(float time)
    {
        animator.Play("Stun");
        canChooseAction = false;
        StartCoroutine(AllowAction(time));
    }

    IEnumerator AllowAction(float time)
    {
        yield return new WaitForSeconds(time);
        canChooseAction = true;
    }
}
