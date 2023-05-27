using FMODUnity;
using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chase : State
{
    private float waitFor = 2f;
    [SerializeField] EventReference Lose;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        goal = GameObject.FindGameObjectWithTag("Player").transform;
        setSpeed(true);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 realGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
        Vector3 direction = realGoal - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), angleSpeed);

        Debug.DrawRay(transform.position, direction, Color.green);

        Vector3 pushVector = direction.normalized * speed;
        transform.Translate(pushVector, Space.World);
        if (direction.magnitude < distance)
        {
            RuntimeManager.PlayOneShot(Lose);
            while (waitFor > 0)
            {
                waitFor -= Time.deltaTime;
            }
            SceneManager.LoadScene("LoseScene");
        }

    }
}
