using System;
using UnityEngine;

namespace CrazyHammer.Core
{
    [Serializable]
    public struct HandsComponent
    {
        public Transform RootTransform;
        public Transform DesiredPositionTransform;
        [HideInInspector] 
        public Vector3 HandsInitialPosition;
        [HideInInspector] 
        public Vector3 Velocity;
    }
}
