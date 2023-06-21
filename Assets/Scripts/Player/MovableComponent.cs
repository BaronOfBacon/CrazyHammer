using System;
using UnityEngine;
using UnityEngine.Splines;

namespace CrazyHammer.Core
{
    [Serializable]
    public struct MovableComponent
    {
        public Vector3 Position;
        public Vector3 ForwardDirection;
        public Vector3 UpDirection;

        public static MovableComponent CalculateParams(float relativePoint, SplineContainer container)
        {
            var result = new MovableComponent();

            var splineRelativePosition = container.Spline.EvaluatePosition(relativePoint);
            result.Position = container.transform.TransformPoint(splineRelativePosition);

            result.ForwardDirection = container.transform.TransformDirection(container.Spline.EvaluateTangent(relativePoint)); 
            result.UpDirection = container.transform.TransformDirection(container.Spline.EvaluateUpVector(relativePoint));

            return result;
        }
    }
}