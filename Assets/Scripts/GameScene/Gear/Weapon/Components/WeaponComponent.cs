using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponComponent : MonoBehaviour
{
    protected WeaponScript weapon;
    protected WeaponAnimationEventHendler animationEventHendler;

    protected bool isAttackActive;

    protected virtual void Awake()
    {
        weapon = GetComponent<WeaponScript>();

        animationEventHendler = GetComponentInChildren<WeaponAnimationEventHendler>();

    }

    public virtual void Init()
    {

    }
    protected virtual void HandleEnter()
    {
        isAttackActive = true;
    }

    protected virtual void HandleExit()
    {
        isAttackActive = false;
    }

    protected virtual void OnEnable()
    {
        weapon.OnEnter += HandleEnter;
        weapon.OnExit += HandleExit;
    }

    protected virtual void OnDisable()
    {
        weapon.OnEnter -= HandleEnter;
        weapon.OnExit -= HandleExit;
    }
}

public abstract class WeaponComponent<T1,T2> : WeaponComponent where T1 : ComponentData where T2: AttackData
{
    protected T1 data;
    protected int currentAttackData;

    protected override void Awake()
    {
        base.Awake();
        data = weapon.WeaponData.GetData<T1>();


    }

    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentAttackData = GetAttackDirectionIndex();
    }

    private int GetAttackDirectionIndex()
    {
        if (PlayerMovementState.Up)
        {
            return 0;
        }
        if (PlayerMovementState.Left)
        {
            return 1;
        }
        if (PlayerMovementState.Down)
        {
            return 2;
        }
        if (PlayerMovementState.Right)
        {
            return 3;
        }

        return 2;
    }
    //dsa
    public override void Init()
    {
        base.Init();

        data = weapon.WeaponData.GetData<T1>();
    }
}
