using Leopotam.Ecs;
using UnityEngine;

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

                var movementOffset = Vector3.Dot(movableComponent.ForwardDirection, movableComponent.Movement)
                    * movableComponent.ForwardDirection;
                var desiredPosition = movableComponent.SpawnPosition + movementOffset * 0.1f;

                movableTransform.position = desiredPosition;
            }
        }
    }
}