using UnityEngine;

namespace GuardControl
{
    public class Chase : State
    {

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            goal = GameObject.Find("Player").transform;
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var position = goal.position;
            var position1 = transform.position;
            Vector3 realGoal = new Vector3(position.x, position1.y, position.z);
            Vector3 direction = realGoal - position1;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), AngleSpeed);

            Debug.DrawRay(position1, direction, Color.green);
            if (direction.magnitude >= Distance)
            {
                Vector3 pushVector = direction.normalized * Speed;
                transform.Translate(pushVector, Space.World);
            }

        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}
