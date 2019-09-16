using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("MyTask")]
    public class HasFindTarget : Conditional
    {
        public SharedTransform targetTransform;

        public SharedBool reversed;

        public override void OnStart()
        {

        }

        public override TaskStatus OnUpdate()
        {
            if (targetTransform.Value == null)
            {
                return reversed.Value?TaskStatus.Success : TaskStatus.Failure;
            }

            return reversed.Value ? TaskStatus.Failure : TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetTransform = null;
        }
    }
}