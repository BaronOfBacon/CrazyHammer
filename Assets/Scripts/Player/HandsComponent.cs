using System;
using UnityEngine;

namespace CrazyHammer.Core
{
    [Serializable]
    public struct HandsComponent
    {
        public Transform RootTransform;
        public Transform DesiredPositionTransform;
        public Transform LocalDesiredPositionTransform;
    }
}
