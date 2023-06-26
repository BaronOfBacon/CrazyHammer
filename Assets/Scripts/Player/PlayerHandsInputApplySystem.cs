using CrazyHammer.Core.Data;
using CrazyHammer.Core.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace CrazyHammer.Core
{
    public class PlayerHandsInputApplySystem : IEcsRunSystem
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
                EcsEntity touchEntity;
                
                switch (spot.Side)
                {
                    case SpotSide.Left when !_rightTouchFilter.IsEmpty():
                        touchEntity = _rightTouchFilter.GetEntity(0);
                        gameTouch = touchEntity.Get<GameTouchComponent>();
                        break;
                    case SpotSide.Right when !_leftTouchFilter.IsEmpty():
                        touchEntity = _leftTouchFilter.GetEntity(0);
                        gameTouch = touchEntity.Get<GameTouchComponent>();
                        break;
                    default:
                        if (!spot.TouchStartPosition.Equals(spot.CurrentPosition))
                            spot.TouchStartPosition = spot.CurrentPosition;
                        continue;
                }
                ref var handsComponent = ref spot.CharacterEntity.Get<HandsComponent>();
                
                if (touchEntity.Has<NewGameTouchComponent>())
                {
                    handsComponent.HandsInitialPosition = handsComponent.DesiredPositionTransform.position;
                }
                
                var touchPositionOffset = gameTouch.StartScreenPosition - gameTouch.ScreenPosition;
                touchPositionOffset *= _gameSettings.HandsSettings.MovementSensitivity;

                var calculatedHandsPosition = handsComponent.HandsInitialPosition - (Vector3)touchPositionOffset;

                var offset = Vector3.ClampMagnitude(calculatedHandsPosition - handsComponent.RootTransform.position, _gameSettings.HandsSettings.MaxHandsDistance);
                var targetPosition = handsComponent.RootTransform.position + offset;
            
                float smoothDampTime = Mathf.Sqrt(_gameSettings.HandsSettings.Mass);

                handsComponent.DesiredPositionTransform.position = Vector3.SmoothDamp(handsComponent.DesiredPositionTransform.position, targetPosition, ref handsComponent.Velocity, smoothDampTime, Mathf.Infinity, Time.deltaTime * _gameSettings.HandsSettings.LerpSpeed);
            }
        }
    }
}
