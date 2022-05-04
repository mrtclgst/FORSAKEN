using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    Animator _anim;
    private void Awake()
    {
        AwakeRef();
    }
    public void Walk(bool walk)
    {
        _anim.SetBool(AnimationsTag.WALK_PARAMETER, walk);
    }
    public void Run(bool run)
    {
        _anim.SetBool(AnimationsTag.RUN_PARAMETER, run);
    }
    public void Attack()
    {
        _anim.SetTrigger(AnimationsTag.ATTACK_TRIGGER);
    }
    public void Dead()
    {
        _anim.SetTrigger(AnimationsTag.DEAD_TRIGGER);
    }
    private void AwakeRef()
    {
        _anim = GetComponent<Animator>();
    }
}
