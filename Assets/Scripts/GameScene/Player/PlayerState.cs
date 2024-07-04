using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerState previousState, currentState;
    protected Player player;

    public static float X,Y;
    protected bool isAnimationFinished;
    protected bool isExitingState;
    
    
    protected float startTime;




    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerState previousState = null)
    {
        this.previousState = previousState;
        this.stateMachine = playerStateMachine;
        this.player = player;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        isExitingState = true;
    }

    public abstract void UpdateFunction();
    public abstract void FixedUpdateFunction();


    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}

