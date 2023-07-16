using CrazyHammer.Core.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace CrazyHammer.Core
{
    internal class PlayerMovementInputApplySystem : IEcsRunSystem
    {
        private readonly EcsFilter<CharacterSpot> _characterSpot = null;
        private readonly EcsFilter<LeftScreenSideTouchComponent> _leftTouchFilter = null;
        private readonly EcsFilter<RightScreenSideTouchComponent> _rightTouchFilter = null;

        public void Run()
        {
            foreach (var index in _characterSpot)
            {
                ref var spot = ref _characterSpot.Get1(index);

                if (spot.CharacterEntity.IsNull() || !spot.CharacterEntity.Has<PlayerTag>()) continue;

                EcsEntity touchEntity;
                GameTouchComponent gameTouch;
                
                switch (spot.Side)
                {
                    case SpotSide.Left when !_leftTouchFilter.IsEmpty():
                        touchEntity = _leftTouchFilter.GetEntity(0);
                        gameTouch = touchEntity.Get<GameTouchComponent>();
                        break;
                    case SpotSide.Right when !_rightTouchFilter.IsEmpty():
                        touchEntity = _rightTouchFilter.GetEntity(0);
                        gameTouch = touchEntity.Get<GameTouchComponent>();
                        break;
                    default:
                        if (!spot.TouchStartPosition.Equals(spot.CurrentPosition))
                            spot.TouchStartPosition = spot.CurrentPosition;
                        continue;
                }
                
                ref var characterComponent = ref spot.CharacterEntity.Get<CharacterComponent>();
                ref var movableComponent = ref spot.CharacterEntity.Get<MovableComponent>();
                
                if (touchEntity.Has<NewGameTouchComponent>())
                {
                    movableComponent.Velocity = 0f;
                }

                var deltaTouchPosition = gameTouch.ScreenPosition - gameTouch.StartScreenPosition;
                var sensitivityTouchPosition = deltaTouchPosition.x * characterComponent.Settings.BodySettings.Sensitivity 
                                                                    * Time.fixedDeltaTime;
                if (spot.Side == SpotSide.Right)
                    sensitivityTouchPosition *= -1;
                
                var previousPosition = spot.CurrentPosition;
                var targetPosition = Mathf.Clamp(spot.TouchStartPosition + sensitivityTouchPosition, 0, 1);
                var smoothDampTime = Mathf.Sqrt(characterComponent.Settings.BodySettings.Mass);
                
                spot.CurrentPosition = Mathf.SmoothDamp(previousPosition, targetPosition, 
                    ref movableComponent.Velocity, smoothDampTime, Mathf.Infinity, 
                    characterComponent.Settings.HandsSettings.LerpSpeed);
                
                var calculatedMovableComponent = MovableComponent.CalculateParams(spot.CurrentPosition, spot.SplineContainer);
                
                movableComponent = calculatedMovableComponent;
            }
        }
    }
}