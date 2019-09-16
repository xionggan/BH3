using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
    [TaskCategory("Basic/Animator")]
    [TaskDescription("Returns success if the specified tag matches the tag of the active state.")]
    public class IsTag : Conditional
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The layer's index")]
        public SharedInt index;
        [Tooltip("The state tag to compare")]
        public SharedString tag;

        private Animator animator;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject)
            {
                animator = currentGameObject.GetComponent<Animator>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator is null");
                return TaskStatus.Failure;
            }

            return animator.GetCurrentAnimatorStateInfo(index.Value).IsTag(tag.Value) ? TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            index = 0;
            tag = "";
        }
    }
}