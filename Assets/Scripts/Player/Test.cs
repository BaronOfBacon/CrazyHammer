using UnityEngine;
using UnityEngine.Splines;

namespace CrazyHammer.Core
{
    public class Test : MonoBehaviour
    {
        public SplineContainer splineContainer;
        
        private float splineLength;

        public GameObject testGO;
        
        [Range(0f,1f), SerializeField]
        private float relativePosition;
        
        private void Start()
        {
            splineLength = splineContainer.Spline.GetLength();
        }

        private void Update()
        {
            var relativePoint = splineLength * relativePosition / splineLength;
            
            var position = splineContainer.Spline.EvaluatePosition(relativePoint);
            position = splineContainer.transform.TransformPoint(position);
            
            var forwardDirection = splineContainer.Spline.EvaluateTangent(relativePoint);
            forwardDirection = splineContainer.transform.TransformDirection(forwardDirection);
            
            var upDirection = splineContainer.Spline.EvaluateUpVector(relativePoint);
            upDirection = splineContainer.transform.TransformDirection(upDirection);
            
            testGO.transform.position = (Vector3)position;
            testGO.transform.up = upDirection;
            testGO.transform.forward = forwardDirection;
        }
    }
}
