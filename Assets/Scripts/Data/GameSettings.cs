using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrazyHammer.Core.Data
{
    [Serializable, CreateAssetMenu(menuName = "Data/Game settings", fileName = "GameSettings")]
    public class GameSettings : SerializedScriptableObject
    {
        [Range(0.1f, 2f)] 
        public float MovementSensitivity;
        public AnimationCurve PlayerMoveSensitivity;
        public float TouchSensitivity;
        [SerializeField]
        public HandsSettings HandsSettings;
        
        [Space] 
        
        [Title("Test"), ShowInInspector] 
        public SpotSide playerFakeInitSide;
    }

    [Serializable]
    public class HandsSettings
    {
        [ShowInInspector]
        public float MaxHandsDistance
        {
            get;
            private set;
        } = 4f;
        [ShowInInspector] 
        public float MovementSensitivity 
        {
            get;
            private set;
        } = 1f;
        [ShowInInspector] 
        public float Mass{
            get;
            private set;
        } = 1f;
        [ShowInInspector] 
        public float LerpSpeed
        {
            get;
            private set;
        } = 10.0f;
    }
}
