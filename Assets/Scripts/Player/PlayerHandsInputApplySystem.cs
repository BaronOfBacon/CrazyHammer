using CrazyHammer.Core.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace CrazyHammer.Core
{
    public class PlayerHandsInputApplySystem : IEcsRunSystem
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
                    case SpotSide.Left when !_rightTouchFilter.IsEmpty():
                        touchEntity = _rightTouchFilter.GetEntity(0);
                        gameTouch = touchEntity.Get<GameTouchComponent>();
                        break;
                    case SpotSide.Right when !_leftTouchFilter.IsEmpty():
                        touchEntity = _leftTouchFilter.GetEntity(0);
                        gameTouch = touchEntity.Get<GameTouchComponent>();
                        break;
                    default:
                        continue;
                }
                
                ref var characterComponent = ref spot.CharacterEntity.Get<CharacterComponent>();
                ref var handsComponent = ref spot.CharacterEntity.Get<HandsComponent>();
                
                if (touchEntity.Has<NewGameTouchComponent>())
                {
                    handsComponent.InitialLocalPosition = handsComponent.DesiredPositionTransform.position 
                                                          - handsComponent.RootTransform.position;
                    handsComponent.SmoothDumpVelocity = Vector3.zero;
                }
                
                Vector3 touchOffset = gameTouch.ScreenPosition - gameTouch.StartScreenPosition;
                touchOffset *= characterComponent.Settings.HandsSettings.Sensitivity * Time.fixedDeltaTime;

                var localPositionWithTouchOffset = handsComponent.InitialLocalPosition + touchOffset;
                localPositionWithTouchOffset = Vector3.ClampMagnitude(localPositionWithTouchOffset, characterComponent.Settings.HandsSettings.MaxHandsDistance);
                
                var targetPosition = handsComponent.RootTransform.position + localPositionWithTouchOffset;
                
                float smoothDampTime = Mathf.Sqrt(characterComponent.Settings.HandsSettings.Mass);
                var current = handsComponent.DesiredPositionTransform.position;

                var smoothDampedPosition = Vector3.SmoothDamp(current, targetPosition,
                    ref handsComponent.SmoothDumpVelocity, smoothDampTime, Mathf.Infinity,
                    characterComponent.Settings.HandsSettings.LerpSpeed);

                handsComponent.DesiredPositionRB.MovePosition(smoothDampedPosition);
            }
        }
    }
}
