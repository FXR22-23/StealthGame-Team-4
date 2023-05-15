using UnityEngine;

namespace GaurdControl
{
    public class State : StateMachineBehaviour
    {
        protected Transform transform;
        private GameObject _gameObject;
        private GuardStateManager _stateManager;

        protected const float Speed = 0.03f;
        protected const float AngleSpeed = 0.075f;
        protected const float Distance = 1.5f;
        protected Transform goal;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            transform = animator.transform;
            _gameObject = animator.gameObject;
            _stateManager = _gameObject.GetComponent<GuardStateManager>();

            goal = _stateManager.target.transform;
        }
    }
}
