using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEventHendler : MonoBehaviour
{
    public event Action OnFinish;
    public event Action OnAttackAction;
    private void AnimationFinishedTrigger() => OnFinish?.Invoke();
    private void AttackActionTrigger() => OnAttackAction?.Invoke();
}
