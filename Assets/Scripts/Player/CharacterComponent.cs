using System;
using Dreamteck.Splines;
using Sirenix.Serialization;
using UnityEngine;
using SplineMesh = Dreamteck.Splines.SplineMesh;

namespace CrazyHammer.Core
{
    [Serializable]
    public struct CharacterComponent
    {
        [OdinSerialize]
        public CharacterSettings Settings;
        public SplineComputer SplineComputer;
        public SplineMesh SplineMesh;
        public Transform RootTransform;
    }
}
