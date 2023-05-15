using System;
using UnityEngine;

namespace GaurdControl
{
    public class GuardStateManager : MonoBehaviour
    {
        //#---- Magic Numbers ------- #
        private const float NotVisibleTime = 10f;
        private const float DelayTime = 2f;
        private const float RestTime = 5f;
        private const float NumberOfPatrols = 3;

        //#--------- Vars ---------- #
        public GameObject target;
        public float viewingFieldDistance = 7;
        public float viewingFieldAngle = 30;
        public Animator animator;
        private float _timerDelay = DelayTime;
        private float _notVisibleTimer = NotVisibleTime;
        private float _restTimer = RestTime;
        private bool _flagOfDelay;
        private const string IsVisible = "visible";
        private const string IsResting = "resting";


        // Start is called before the first frame update

        // #--------------- Our Methods -------------------- #
        public bool IsInSight()
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

        // Update is called once per frame
        void Update()
        {
            if (Math.Abs(PatrolsCounter.Get() - NumberOfPatrols) > 0x0)
            {
                //Todo Remove Yellow Color
                var transform1 = transform;
                Debug.DrawRay(transform1.position, transform1.forward * viewingFieldDistance);
                transform.Rotate(0, 1f, 0);
                //helping to wait for the guard to enter chase mode - animator skips chase mode without it.
                if (_flagOfDelay)
                {
                    if (_timerDelay >= 0)
                    {
                        _timerDelay -= Time.deltaTime;
                        //Debug.Log(timerDelay);
                    }
                    else
                    {
                        _flagOfDelay = false;
                        _timerDelay = DelayTime;
                    }
                }
                else
                {
                    if (IsInSight())
                    {
                        animator.SetBool(IsVisible, true);
                        _flagOfDelay = true;
                        _notVisibleTimer = NotVisibleTime;
                        //workTimer = WORK_TIME;
                    }
                    else
                    {
                        //workTimer -= Time.deltaTime;
                        //after 10 seconds of chasing without the target in sight stops chasing. and back to patrol
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
            else
            {

                if(_restTimer > 0)
                {
                    _restTimer -= Time.deltaTime;
                    animator.SetBool(IsResting, true);
                
                }
                else
                {
                    _restTimer = RestTime;
                    PatrolsCounter.Reset();
                    animator.SetBool(IsResting, false);
                }
            }
        }
    }
}
