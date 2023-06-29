using CrazyHammer.Core.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrazyHammer.Core
{
    [CreateAssetMenu(menuName = "Data/CharacterSettings", fileName = "CharacterSettings"), InlineEditor()]
    public class CharacterSettings : SerializedScriptableObject
    {
        [SerializeField] 
        public BodySettings BodySettings;
        [SerializeField] 
        public HandsSettings HandsSettings;
    }
}
