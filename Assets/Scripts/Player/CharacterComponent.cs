using System;
using Dreamteck.Splines;
using Sirenix.Serialization;

namespace CrazyHammer.Core
{
    [Serializable]
    public struct CharacterComponent
    {
        [OdinSerialize]
        public CharacterSettings Settings;
        public SplineComputer SplineComputer;
    }
}
