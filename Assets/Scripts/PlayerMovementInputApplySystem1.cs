using CrazyHammer.Core.Data;
using CrazyHammer.Core.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace CrazyHammer.Core
{
    internal class PlayerMovementInputApplySystem1 : IEcsRunSystem
    {
        private GameSettings _gameSettings = null;
        
        private readonly EcsFilter<CharacterSpot> _characterSpot = null;
        private readonly EcsFilter<LeftScreenSideTouchComponent> _leftTouchFilter = null;
        private readonly EcsFilter<RightScreenSideTouchComponent> _rightTouchFilter = null;

        public void Run()
        {
            foreach (var index in _characterSpot)
            {
                ref var spot = ref _characterSpot.Get1(index);

                if (spot.CharacterEntity.IsNull() || !spot.CharacterEntity.Has<PlayerTag>()) continue;

                GameTouchComponent gameTouch;
                
                switch (spot.Side)
                {
                    case SpotSide.Left when !_leftTouchFilter.IsEmpty():
                        gameTouch = _leftTouchFilter.GetEntity(0).Get<GameTouchComponent>();
                        break;
                    case SpotSide.Right when !_rightTouchFilter.IsEmpty():
                        gameTouch = _rightTouchFilter.GetEntity(0).Get<GameTouchComponent>();
                        break;
                    default:
                        if (!spot.TouchStartPosition.Equals(spot.CurrentPosition))
                            spot.TouchStartPosition = spot.CurrentPosition;
                        continue;
                }

                var deltaTouchPosition = gameTouch.ScreenPosition - gameTouch.StartScreenPosition;
                var sensitivityTouchPosition = deltaTouchPosition.x * _gameSettings.TouchSensitivity;
                if (spot.Side == SpotSide.Right)
                    sensitivityTouchPosition *= -1;
                spot.CurrentPosition = Mathf.Clamp(spot.TouchStartPosition + sensitivityTouchPosition, 0, 1);
                ref var movableComponent = ref spot.CharacterEntity.Get<MovableComponent>();
                
                movableComponent = MovableComponent.CalculateParams(spot.CurrentPosition, spot.MovementSpline);
            }
        }
    }
}