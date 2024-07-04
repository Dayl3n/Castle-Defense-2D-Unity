using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : PlayerState
{
    

    private Animator animator;
    private bool isAttackActive;

    public static bool Up => Y == 1 && X == 0;
    public static bool Down => Y == -1 && X == 0;
    public static bool Left => Y == 0 && X == -1;
    public static bool Right => Y == 0 && X == 1;

    private Vector2 move, smoothedMovementInput, smoothMovementInputVelocity;

    public PlayerMovementState(Player player, PlayerStateMachine playerStateMachine, Animator animator, PlayerState previousState = null) : base(player, playerStateMachine, previousState)
    {
        this.animator = animator;
    }
    // Update is called once per frame
    public override void UpdateFunction()
    {
        //getting inputs
        move.x = player.inputHandler.NormInputX;
        move.y = player.inputHandler.NormInputY;

        if (player.inputHandler.AttackInput)
        {
            isExitingState = true;
            stateMachine.ChangeState(player.combat);
        }

 
        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastMoveX", move.x);
            animator.SetFloat("LastMoveY", move.y);

            X = move.x;
            Y = move.y;
        }

        animator.SetFloat("Speed", move.sqrMagnitude);
    }

    public override void Exit()
    {
        base.Exit();

        animator.SetBool("leave", isExitingState);
    }

    public override void Enter()
    {
        base.Enter();

        animator.SetBool("leave", isExitingState);
    }



    public override void FixedUpdateFunction()
    {
        //Movement
        if (!ShopEnterLeaveScript.isMenuOpen||!isAttackActive)
        {
            smoothedMovementInput = Vector2.SmoothDamp(
                smoothedMovementInput,
                move,
                ref smoothMovementInputVelocity,
                0.1f);


            player.Rb.velocity = smoothedMovementInput * player.Speed;
        }
        else
        {
            player.Rb.velocity = Vector2.zero;
        }
    }

}
