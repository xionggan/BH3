using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("MyTask")]
    public class IsInAtkRange : Conditional
    {
        [Tooltip("The GameObject that try to attack")]
        public SharedTransform targetTransform;

        public SharedInt atkRange;

        public SharedBool reversed;

        private GameObject prevGameObject;

        public override void OnStart()
        {
            if (prevGameObject != gameObject)
            {
                prevGameObject = gameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (targetTransform.Value == null)
            {
                Debug.LogWarning("targetTransform is null");
                return TaskStatus.Failure;
            }

            float distance = Vector3.Distance(transform.position, targetTransform.Value.position);

            if (distance < atkRange.Value)
            {
                return reversed.Value ? TaskStatus.Failure : TaskStatus.Success;
            }
            else
            {
                return reversed.Value ? TaskStatus.Success : TaskStatus.Failure;
            }
        }

        public override void OnReset()
        {
            targetTransform = null;
            atkRange = 0;
        }
    }
}