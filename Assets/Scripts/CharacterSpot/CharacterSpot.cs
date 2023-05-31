using System;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
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
    }
}
