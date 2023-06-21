using System;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Splines;

namespace CrazyHammer.Core
{
    [Serializable]
    public struct CharacterSpot
    {
        [ShowInInspector]
        public SplineContainer MovementSpline;
        [ShowInInspector] 
        public SpotSide Side;
        [ShowInInspector]
        public EcsEntity CharacterEntity;
        [ShowInInspector, Range(0f, 1f)]
        public float CurrentPosition;
        [ShowInInspector, Range(0f, 1f)]
        public float TouchStartPosition;
    }
}
