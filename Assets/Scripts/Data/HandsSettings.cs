using System;
using UnityEngine;

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
            get => sensitivity;
        }
        public float Mass
        {
            get => _mass;
        }
        public float LerpSpeed
        {
            get => _lerpSpeed;
        }
        
        [SerializeField]
        private float _maxHandsDistance = 4f;
        [SerializeField]
        private float sensitivity = 1f;
        [SerializeField]
        private float _mass = 1f;
        [SerializeField]
        private float _lerpSpeed = 8f;
    }
}