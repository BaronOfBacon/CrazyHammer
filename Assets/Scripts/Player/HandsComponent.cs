using System;
using UnityEngine;

namespace CrazyHammer.Core
{
    [Serializable]
    public struct HandsComponent
    {
        public Transform RootTransform;
        public Transform DesiredPositionTransform;
        public Rigidbody DesiredPositionRB;
        public Transform BackHandsBoundary;
        public Transform LowHandsBoundary;
        [HideInInspector] 
        public Vector3 InitialLocalPosition;
        [HideInInspector] 
        public Vector3 SmoothDumpVelocity;
    }
}
