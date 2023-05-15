using UnityEngine;

namespace GuardControl
{
    public class Patrol : State
    {
        public GameObject[] wayPoints;
        private int _nextWayPoint;
        public Transform goalWayPoint;

        private void Awake()
        {
            wayPoints = GameObject.FindGameObjectsWithTag("waypoint");
            foreach (var wayPoint in wayPoints)
            {
                Debug.Log(wayPoint.name + " ");
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            goalWayPoint = wayPoints[_nextWayPoint].transform;
            var position = transform.position;
            var position1 = goalWayPoint.position;
            Vector3 realGoal = new Vector3(position1.x, position.y, position1.z);
            Vector3 direction = realGoal - position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), AngleSpeed);

            Debug.DrawRay(position, direction, Color.green);
            if(direction.magnitude >= Distance)
            {
                Vector3 pushVector = direction.normalized * Speed;
                transform.Translate(pushVector, Space.World);
            }
            else
            {
                //Changing the way point if the distance from the point is lower than 2 meters.(for nice slerp)
                //assuming the number of way points is odd.
                _nextWayPoint += 2;
                _nextWayPoint %= wayPoints.Length;
                if (_nextWayPoint == 0)
                {
                    PatrolsCounter.Increment();
                }
            }


        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}
