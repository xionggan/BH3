using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("MyTask")]
    public class LookAtPlayer : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;

        public SharedVector3 Offset;

        [Tooltip("Transform to look at")]
        public SharedTransform lookTransform;
        public SharedFloat angleSpeed;
        public SharedBool immediately;

        private Transform targetTransform;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject)
            {
                targetTransform = currentGameObject.GetComponent<Transform>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (targetTransform == null)
            {
                Debug.LogWarning("Transform is null");
                return TaskStatus.Failure;
            }

            Vector3 lookDir = lookTransform.Value.position - (targetTransform.position + Offset.Value);

            if (immediately.Value)
            {
                targetTransform.forward = lookDir;
            }
            else
            {
                targetTransform.forward = Vector3.RotateTowards(targetTransform.forward, lookDir,
                    angleSpeed.Value * Time.deltaTime,0);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            lookTransform = null;
        }
    }
}