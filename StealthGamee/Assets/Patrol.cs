using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    public GameObject[] wayPoints;
    int nextWayPoint = 0;
    public Transform goalWayPoint;

    private void Awake()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("waypoint");
        for (int i = 0; i < wayPoints.Length; i++)
        {
            Debug.Log(wayPoints[i].name + " ");
        }
    }


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        goalWayPoint = wayPoints[nextWayPoint].transform;
        Vector3 realGoal = new Vector3(goalWayPoint.position.x, transform.position.y, goalWayPoint.position.z);
        Vector3 direction = realGoal - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), angleSpeed);

        Debug.DrawRay(transform.position, direction, Color.green);
        if(direction.magnitude >= distance)
        {
            Vector3 pushVector = direction.normalized * speed;
            transform.Translate(pushVector, Space.World);
        }
        else
        {
            //Changing the way point if the distance from the point is lower than 2 meters.(for nice slerp)
            //assuming the number of way points is odd.
            nextWayPoint += 2;
            nextWayPoint %= wayPoints.Length;
            if (nextWayPoint == 0)
            {
                patrolsCounter.counter++ ;
            }
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
