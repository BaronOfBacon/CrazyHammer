using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrazyHammer.Core.Data
{
    [Serializable, CreateAssetMenu(menuName = "Data/Game settings", fileName = "GameSettings")]
    public class GameSettings : SerializedScriptableObject
    {
        public float MovementSensitivity;
        [SerializeField] 
        public BodySettings BodySettings;
        [SerializeField]
        public HandsSettings HandsSettings;
        [SerializeField]
        public Vector2Int TargetScreenRatio = new Vector2Int(1920,1080);
        
        [Space] 
        
        [Title("Test"), ShowInInspector] 
        public SpotSide PlayerFakeInitSide;
        public float MouseSensitivity;
    }

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
    [Serializable]
    public class BodySettings
    {
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
        private float sensitivity = 1f;
        [SerializeField]
        private float _mass = 1f;
        [SerializeField]
        private float _lerpSpeed = 8f;
    }
}
