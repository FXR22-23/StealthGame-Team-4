using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class State : StateMachineBehaviour
{
    protected Transform transform;
    protected GameObject gameObject;
    protected GuardStateManager stateManager;
    private float SPEED = 0.045f;
    protected float speed = 0.045f;
    protected float angleSpeed = 0.09f;
    protected float distance = 1.5f;
    protected Transform goal;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        gameObject = animator.gameObject;
        stateManager = gameObject.GetComponent<GuardStateManager>();

        goal = stateManager.target.transform;
    }

    public void setSpeed(bool faster)
    {   if (faster)
        { 
            speed += 0.015f;
        }
        else
        {
            speed = SPEED;
        }
        
    }
}
