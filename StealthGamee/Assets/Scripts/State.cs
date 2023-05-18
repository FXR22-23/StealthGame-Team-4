using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : StateMachineBehaviour
{
    protected Transform transform;
    protected GameObject gameObject;
    protected GuardStateManager stateManager;

    protected float speed = 0.03f;
    protected float angleSpeed = 0.075f;
    protected float distance = 1.5f;
    protected Transform goal;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        gameObject = animator.gameObject;
        stateManager = gameObject.GetComponent<GuardStateManager>();

        goal = stateManager.target.transform;
    }
}
