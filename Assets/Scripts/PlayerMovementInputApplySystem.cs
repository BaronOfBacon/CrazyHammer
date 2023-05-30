using CrazyHammer.Core.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace CrazyHammer.Core
{
    internal class PlayerMovementInputApplySystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovableComponent, PlayerTag> _playerComponents = null;
        private readonly EcsFilter<GameTouchComponent, LeftScreenSideTouchComponent> _touches = null;

        public void Run()
        {
            foreach (var touchIndex in _touches)
            {
                var touch = _touches.Get1(touchIndex);
                var touchOffset = touch.ScreenPosition - touch.StartScreenPosition;
                
                foreach (var playerIndex in _playerComponents)
                {
                    ref var playerMovable = ref _playerComponents.Get1(playerIndex);
                    Vector3 offset = playerMovable.ForwardDirection * touchOffset 
                                                                * playerMovable.MovementMultiplier;
                    playerMovable.Movement = playerMovable.SpawnPosition + offset;
                }
            }
        }
    }
}