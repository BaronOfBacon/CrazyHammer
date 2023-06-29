using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace CrazyHammer.Core
{
    [Serializable]
    public struct CharacterComponent
    {
        [OdinSerialize]
        public CharacterSettings Settings;
    }
}
