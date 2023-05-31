using Leopotam.Ecs;
using Unity.Mathematics;

namespace CrazyHammer.Core
{
    public class RotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, MovableComponent> _movables = null;
        
        public void Run()
        {
            foreach (var index in _movables)
            {
                ref var movableTransform = ref _movables.Get1(index).Transform;
                ref var movableComponent = ref _movables.Get2(index);

                movableTransform.rotation = quaternion.LookRotation(movableComponent.ForwardDirection, 
                    movableComponent.UpDirection);
            }
        }
    }
}
