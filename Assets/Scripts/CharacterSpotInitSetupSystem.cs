using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Splines;

namespace CrazyHammer.Core
{
    public class CharacterSpotInitSetupSystem : IEcsInitSystem
    {
        private EcsFilter<CharacterSpot> _filter;
        
        public void Init()
        {
            foreach (var index in _filter)
            {
                var spot = _filter.Get1Ref(index);

                if (spot.Unref().CharacterEntity.IsNull()) continue;
                
                ref var movableComponent = ref spot.Unref().CharacterEntity.Get<MovableComponent>();
                ref var movementSplineContainer = ref spot.Unref().MovementSpline;
                
                var initMovableParams = CalculateInitParams(0, movementSplineContainer);
                
                movableComponent.SpawnPosition = initMovableParams.SpawnPosition;
                movableComponent.Movement = Vector3.zero;

                movableComponent.ForwardDirection = initMovableParams.ForwardDirection;
                movableComponent.UpDirection = initMovableParams.UpDirection;
            }
        }
        
        MovableComponent CalculateInitParams(float relativePoint, SplineContainer container)
        {
            var initMovableParams = new MovableComponent();
                
            var splineRelativePosition = container.Spline.EvaluatePosition(relativePoint);
            initMovableParams.SpawnPosition = container.transform.TransformPoint(splineRelativePosition);

            initMovableParams.ForwardDirection = container.transform.TransformDirection(container.Spline.EvaluateTangent(relativePoint)); 
            initMovableParams.UpDirection = container.transform.TransformDirection(container.Spline.EvaluateUpVector(relativePoint));
            
            /*initMovableParams.ForwardDirection = container.Spline.EvaluateTangent(relativePoint); 
            initMovableParams.UpDirection = container.Spline.EvaluateUpVector(relativePoint);*/

            return initMovableParams;
        }
    }
}
