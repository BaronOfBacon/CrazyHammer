using CrazyHammer.Core.Input;
using Leopotam.Ecs;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif
using UnityEngine;
using Voody.UniLeo;

namespace CrazyHammer.Core
{
    public class ECSGameStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();
            
            #if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
            #endif
            
            _systems = new EcsSystems(_world);

            _systems.ConvertScene();

            AddInjections();
            AddOneFrames();
            AddSystems();
            
            _systems.Init();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void AddSystems()
        {
            _systems
                .Add(new EnableGameFieldInputSystem())
                .Add(new CreateAndDeleteGameTouchesSystem())
                .Add(new UpdateGameTouchesPositionSystem())
                .Add(new GameTouchScreenSideSetSystem());
        }

        private void AddInjections()
        {
        }
        
        private void AddOneFrames()
        {
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems = null;
            _world?.Destroy();
            _world = null;
        }
    }
}
