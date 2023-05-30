using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrazyHammer.Core.Data
{
    [Serializable, CreateAssetMenu(menuName = "Data/Game settings", fileName = "GameSettings")]
    public class GameSettings : SerializedScriptableObject
    {
        [Range(0.1f, 2f)]
        public readonly float MovementSensitivity;
    }
}
