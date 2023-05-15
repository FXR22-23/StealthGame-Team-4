using UnityEngine;

namespace GuardControl
{
    public class Alarm : State
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        /*
         * Make alarm goes off. 
         */
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("alarm");
        }
    }
}
