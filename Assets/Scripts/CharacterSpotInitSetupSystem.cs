using Leopotam.Ecs;

namespace CrazyHammer.Core
{
    public class CharacterSpotInitSetupSystem : IEcsInitSystem
    {
        private EcsFilter<CharacterSpot> _filter;
        
        public void Init()
        {
            foreach (var index in _filter)
            {
                ref var spot = ref _filter.Get1(index);
                
                spot.CurrentPosition = 0f;
                
                if (spot.CharacterEntity.IsNull()) continue;
                
                ref var movableComponent = ref spot.CharacterEntity.Get<MovableComponent>();
                ref var movementSplineContainer = ref spot.MovementSpline;
                
                movableComponent = MovableComponent.CalculateParams(0, movementSplineContainer);
            }
        }
    }
}
