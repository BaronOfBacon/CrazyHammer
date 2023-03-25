using Leopotam.Ecs;

namespace CrazyHammer.Core.Input
{
    public class EnableGameFieldInputSystem: IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init()
        {
            _world.NewEntity().Get<ProcessGameFieldInputComponent>();
        }
    }
}