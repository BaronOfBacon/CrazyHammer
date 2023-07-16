using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyHammer.Core
{
    public class PlainBonesMovementTest : MonoBehaviour
    {
        public Transform bodyTransform;
        public Transform shoulderTargetTransform;
        public Transform elbowTargetTransform;
        public Transform wristTargetTransform;

        public List<Transform> transforms;
        
        public Vector3 shoulderPosition;
        public Vector3 elbowPosition;
        public Vector3 wristPosition;

        public float radius = 0.1f;
        public bool draw;
        public int colliderDirection;

        private void Start()
        {
            
        }

        private void OnDrawGizmos()
        {
            Vector3 bodyPosition = bodyTransform.position;
            

            var jointsList = new List<Transform>()
            {
                shoulderTargetTransform,
                elbowTargetTransform,
                wristTargetTransform
            };

            var positionsList = new List<Vector3>()
            {
                shoulderPosition,
                elbowPosition,
                wristPosition
            };

            for (int i = 2; i >= 0; i--)
            {
                var position = jointsList[i].position;
                position.z = bodyPosition.z;
                positionsList[i] = position;
            }

            if (draw)
                AdjustColliders(jointsList, positionsList);

            DrawPositions(positionsList);
        }
        
        private void DrawPositions(List<Vector3> list)
        {
            for (int i = 2; i >= 0; i--)
            {
                Gizmos.DrawWireSphere(list[i], radius);
            } 
        }

        private void AdjustColliders(List<Transform> rootTransform, List<Vector3> positions)
        {
            for (var i = 0; i < positions.Count - 1; i++)
            {
                var direction = positions[i + 1] - positions[i];
                var worldPosition = positions[i] + direction / 2f;

                var target = transforms[i];
                target.position = worldPosition;
                var tempScale = target.localScale;
                tempScale.z = direction.magnitude;
                target.localScale = tempScale;
                target.forward = direction;

                /*var startPosition = worldPosition + direction / 2f;
                var finishPosition = worldPosition - direction / 2f;

                var firstOffset = Vector3.Cross(targetTransform.right, direction).normalized;
                firstOffset *= radius;
                
               var secondOffset = Vector3.Cross(targetTransform.up, direction).normalized;
               secondOffset *= radius;
                
                Gizmos.DrawLine(startPosition, finishPosition);
                Gizmos.DrawLine(startPosition + firstOffset, finishPosition + firstOffset);
                Gizmos.DrawLine(startPosition - firstOffset, finishPosition - firstOffset);
                Gizmos.DrawLine(startPosition + secondOffset, finishPosition + secondOffset);
                Gizmos.DrawLine(startPosition - secondOffset, finishPosition - secondOffset); */
            }
        }
    }
}
