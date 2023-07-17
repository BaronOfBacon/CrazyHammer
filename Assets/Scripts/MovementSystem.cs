using Leopotam.Ecs;

namespace CrazyHammer.Core
{
    internal class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, MovableComponent> _movables = null;
        
        public void Run()
        {
            foreach (var index in _movables)
            {
                ref var movableTransform = ref _movables.Get1(index).Transform;
                ref var movableComponent = ref _movables.Get2(index);
               
                movableTransform.position = movableComponent.Position;
            }
        }
    }
}