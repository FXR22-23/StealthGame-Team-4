using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;



public class GuardStateManager : MonoBehaviour
{
    //#---- Magic Numbers ------- #
    const float NOT_VISIBLE_TIME = 10f;
    const float DELAY_TIME = 2f;
    const float REST_TIME = 5f;
    const float NUMBER_OF_PATROLS = 3;

    //#--------- Vars ---------- #
    public GameObject target;
    public float viewingFieldDistance = 7;
    public float hearingFieldDistance = 3;
    public float viewingFieldAngle = 30;
    public Animator animator;
    float timerDelay = DELAY_TIME;
    float notVisibleTimer = NOT_VISIBLE_TIME;
    float restTimer = REST_TIME;
    bool flagOfDelay = false;


    // Start is called before the first frame update

    // #--------------- Our Methods -------------------- #
    public bool IsInSight()
    {
        //1. Distance
        float distance = Vector3.Distance(transform.position,target.transform.position);
        if (distance > viewingFieldDistance)
        {
            return false;
        }

        //2. Angle;
        Vector3 distanceVector = target.transform.position - transform.position;
        distanceVector.y = 0;
        float angle = Vector3.Angle(transform.forward,distanceVector);
        //Debug.Log("This is angle " + angle); \2
        if (angle > viewingFieldAngle)
        {
            return false;
        }
        //3. Obstacles 

        RaycastHit hit;
        if (Physics.Raycast(transform.position,distanceVector.normalized,out hit,distance))
        {
            Debug.Log(hit.collider.gameObject.name + " ?= " + target); 
            Debug.Log( hit.collider.gameObject == target); 
            
            return (hit.collider.gameObject == target);
        }
        return false;
    }

    public bool IsHeard()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= hearingFieldDistance;
    }

    public void SetAlarm()
    {
        animator.SetBool("alarmed", true);
    }


    // #-------------- Start and Update Funcs --------------#
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolsCounter.counter != NUMBER_OF_PATROLS)
        {
            //Debug.Log("Patrols-Counter= " + patrolsCounter.counter);
            //Todo Remove Yellow Color
            //Debug.DrawRay(transform.position, transform.forward * viewingFieldDistance);
            //transform.Rotate(0, 1f, 0);


            //helping to wait for the guard to enter chase mode - animator skips chase mode without it.
            if (flagOfDelay)
            {
                if (timerDelay >= 0)
                {
                    timerDelay -= Time.deltaTime;
                }
                else
                {
                    flagOfDelay = false;
                    timerDelay = DELAY_TIME;
                }
            }
            else
            {
                if (IsInSight() || IsHeard())

                {
                    animator.SetBool("visible", true);
                    flagOfDelay = true;
                    notVisibleTimer = NOT_VISIBLE_TIME;
                }
                else
                {
                    
                    //after 10 seconds of chasing without the target in sight stops chasing. and back to patrol
                    if (notVisibleTimer < 0)
                    {

                        animator.SetBool("visible", false);

                    }
                    else
                    {
                        notVisibleTimer -= Time.deltaTime;
                    }
                }
            }
        }
        else
        {

            if(restTimer > 0)
            {
                restTimer -= Time.deltaTime;
                animator.SetBool("resting", true);
                
            }
            else
            {
                restTimer = REST_TIME;
                patrolsCounter.counter = 0;
                animator.SetBool("resting", false);
            }
        }
    }
}
