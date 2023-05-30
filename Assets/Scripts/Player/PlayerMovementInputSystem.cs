using Leopotam.Ecs;

namespace CrazyHammer.Core
{
    public class PlayerMovementInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<MovableComponent, PlayerTag> _movables = null;
        

        public void Init()
        {
            
        }
        
        public void Run()
        {
            
            foreach (var index in _movables)
            {
                ref var component = ref _movables.Get1(index);
                
                //component.Movement = 
            }
        }

        
    }
}