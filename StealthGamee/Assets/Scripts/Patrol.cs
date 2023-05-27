using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    public GameObject[] wayPoints;
    int nextWayPoint = 0;
    public Transform goalWayPoint;
    public float timer = 1f;

    [SerializeField] EventReference walk;
    [SerializeField] EventReference breath;

    private void Awake()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("waypoint");
        
    }


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            RuntimeManager.PlayOneShot(walk);
            RuntimeManager.PlayOneShot(breath);
            timer = 1f;
        }


        goalWayPoint = wayPoints[nextWayPoint].transform;
        //Debug.Log("This is Game Object: " + goalWayPoint.gameObject);
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
            nextWayPoint += 1;
            nextWayPoint %= wayPoints.Length;
            //Debug.Log(wayPoints[nextWayPoint].name + " \\" + wayPoints.Length);
            if (nextWayPoint == 0)
            {
                patrolsCounter.counter++ ;
            }
        }
    }
}
