using UnityEngine;

namespace CrazyHammer.Core.Input
{
    internal struct GameTouchComponent
    {
        public Vector2 StartScreenPosition;
        public Vector2 ScreenPosition;
        public Vector2 DeltaPositionLastFrame;
        public int ID;
    }
}