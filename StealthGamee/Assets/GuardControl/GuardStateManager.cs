using System;
using UnityEngine;

namespace GuardControl
{
    public class GuardStateManager : MonoBehaviour
    {
        //#---- Magic Numbers ------- #
        private const float NotVisibleTime = 10f;
        private const float DelayTime = 2f;
        private const float RestTime = 5f;
        private const float NumberOfPatrols = 3;
        private const string IsVisible = "visible";
        private const string IsResting = "resting";
        private const string IsAlarmOff = "alarm";
        //#--------- Vars ---------- #
        public GameObject target;
        public float viewingFieldDistance = 7;
        public float viewingFieldAngle = 30;
        public Animator animator;
        private float _timerDelay = DelayTime;
        private float _notVisibleTimer = NotVisibleTime;
        private float _restTimer = RestTime;
        private bool _flagOfDelay;
        private static bool _alarm; // Common among all the guards.
        // Start is called before the first frame update

        // #--------------- Our Methods -------------------- #
        private bool IsInSight()
        {
            //1. Distance
            var distance = Vector3.Distance(transform.position,target.transform.position);
            if (distance > viewingFieldDistance)
            {
                return false;
            }

            //2. Angle;
            var transform1 = transform;
            var distanceVector = target.transform.position - transform1.position;
            distanceVector.y = 0;
            var angle = Vector3.Angle(transform1.forward,distanceVector);
            //Debug.Log("This is angle " + angle);
            if (angle > viewingFieldAngle / 2)
            {
                return false;
            }
            //3. Obstacles 

            RaycastHit hit;
            if (!Physics.Raycast(
                    transform.position, distanceVector.normalized, out hit, distance)) 
                return false;
            Debug.Log(hit.collider.gameObject.name + " ?= " + target); 
            Debug.Log( hit.collider.gameObject == target); 
            
            return (hit.collider.gameObject == target);
        }

        // #-------------- Start and Update Funcs --------------#
        
        /*
         * Update is called once per frame
         * We use update to change the states of the Guard
         */
        void Update()
        {
            if (_alarm) // When alarm is off, the guard consistently in chasing mode
            {
                animator.SetBool(IsAlarmOff, true);
            }
            else 
            {
                if (PatrolsCounter.Get() != NumberOfPatrols) // Check if it is a time for break
                                                             // (Guard has done NumberOfPatrols patrols)
                {
                    var transform1 = transform;
                    Debug.DrawRay(transform1.position, transform1.forward * viewingFieldDistance);
                    transform.Rotate(0, 1f, 0);
                    // If _flagOfDelay, then the Guard needs to stay in chains mode.
                    // This is for the delay (when the switch is immediately, we cannot see the Guard chases).
                    if (_flagOfDelay)
                    {
                        if (_timerDelay >= 0)
                        {
                            _timerDelay -= Time.deltaTime;
                        }
                        else
                        {
                            _flagOfDelay = false;
                            _timerDelay = DelayTime;
                        }
                    }
                    else
                    {
                        if (IsInSight()) // Do the guard see the player?
                        {
                            animator.SetBool(IsVisible, true);
                            _flagOfDelay = true;
                            _notVisibleTimer = NotVisibleTime;
                        }
                        else // After not seeing the player for a long time, get back to patrolling
                        {
                            if (_notVisibleTimer < 0)
                            {

                                animator.SetBool(IsVisible, false);

                            }
                            else
                            {
                                _notVisibleTimer -= Time.deltaTime;
                            }
                        }
                    }
                }
                else // Rest
                {
                    if(_restTimer > 0) // Have some time to rest
                    {
                        _restTimer -= Time.deltaTime;
                        animator.SetBool(IsResting, true);
                
                    }
                    else // Get back to work
                    {
                        _restTimer = RestTime;
                        PatrolsCounter.Reset();
                        animator.SetBool(IsResting, false);
                    }
                }
            }
        }
        
        /*
         * When setting the alarm off (i.e. grabbing the object by the user),
         * the _alarm property is set to be true for all the guards.
         */

        public static void SetAlarm(bool alarm)
        {
            _alarm = alarm;
        }
    }
}
