using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Splines;

namespace CrazyHammer.Core
{
    [Serializable]
    public struct CharacterSpot
    {
        [SerializeField]
        public Transform Position;
        [ShowInInspector]
        public SplineContainer MovementSpline; 
    }
}
