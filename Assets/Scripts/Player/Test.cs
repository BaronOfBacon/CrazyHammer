using UnityEngine;

namespace CrazyHammer.Core
{
    public class Test : MonoBehaviour
    {
        public ConfigurableJoint joint;
        public Transform targetTransform;
        private Quaternion initialRotation;
        
        private void Awake()
        {
            initialRotation = targetTransform.localRotation;
        }

        private void Update()
        {
            joint.targetRotation = Quaternion.Inverse(targetTransform.localRotation) * initialRotation;
        }

    }
}
