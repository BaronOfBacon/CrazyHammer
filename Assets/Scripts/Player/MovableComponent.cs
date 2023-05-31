using System;
using UnityEngine;

namespace CrazyHammer.Core
{
    [Serializable]
    public struct MovableComponent
    {
        public Vector3 SpawnPosition;
        public Vector3 Movement;
        public Vector3 ForwardDirection;
        public Vector3 UpDirection;
        public float MovementMultiplier;
    }
}