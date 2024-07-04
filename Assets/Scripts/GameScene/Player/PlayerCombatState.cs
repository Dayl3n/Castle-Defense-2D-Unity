using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatState : PlayerState
{

    [SerializeField]
    private WeaponScript weapon;
    public WeaponScript Weapon { get => weapon; }

    public PlayerCombatState(Player player,PlayerStateMachine stateMachine,WeaponScript weapon, PlayerState previousState = null) : base(player, stateMachine,previousState)
    {       
        this.weapon = weapon;
    }


    public void ChangeWeapon(WeaponSO newMainWeapon)
    {
       // weapon.ChangeWeapon(newMainWeapon);
    }

    public override void UpdateFunction()
    {
        weapon.Enter();
       
    }

    public override void Enter()
    {
        base.Enter();

        weapon.OnExit += ExitHandler;
        weapon.Anim.SetFloat("LastMoveX", X);
        weapon.Anim.SetFloat("LastMoveY", Y);
    }

    public override void Exit()
    {
        base.Exit();
        isExitingState = true;
    }



    public override void FixedUpdateFunction()
    {
        player.Rb.velocity = Vector2.zero;
    }

    private void ExitHandler()
    {     
        AnimationFinishTrigger();
        stateMachine.ChangeState(player.movement);
    }
}
