using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CrazyHammer.Core.Data
{
    [Serializable]
    public class HandsSettings
    {
        public float MaxHandsDistance
        {
            get => _maxHandsDistance;
        }
        public float Sensitivity
        {
            get => _sensitivity;
        }
        public float SmoothTime
        {
            get => _smoothTime;
        }
        public float LerpSpeed
        {
            get => _lerpSpeed;
        }
        public LayerMask HandsBoundariesMask
        {
            get => _handsBoundariesMask;
        }
        
        [SerializeField]
        private float _maxHandsDistance = 4f;
        [FormerlySerializedAs("sensitivity")] [SerializeField]
        private float _sensitivity = 1f;
        [SerializeField]
        private float _smoothTime = 1f;
        [SerializeField]
        private float _lerpSpeed = 8f;
        [SerializeField] 
        private LayerMask _handsBoundariesMask;
    }
}