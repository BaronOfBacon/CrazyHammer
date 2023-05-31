using CrazyHammer.Core.Data;
using CrazyHammer.Core.Input;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif
using UnityEngine;
using Voody.UniLeo;

namespace CrazyHammer.Core
{
    public class ECSGameStartup : SerializedMonoBehaviour
    {
        [SerializeField, ShowInInspector] 
        private GameSettings _gameSettings;
        
        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedUpdateSystems;

        private void Start()
        {
            _world = new EcsWorld();
            
            #if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
            #endif
            
            _systems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            
            _systems.ConvertScene();
            
            AddInjections();
            AddOneFrames();
            AddSystems();
            
            _systems.Init();
            _fixedUpdateSystems.Init();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems.Run();
        }

        private void AddSystems()
        {
            _systems
                .Add(new EnableGameFieldInputSystem())
                .Add(new CUDGameTouchesSystem())
                #if UNITY_EDITOR
                //inspector debug
                .Add(new CUDMouseGameTouchesSystem())
                #endif
                .Add(new GameTouchScreenSideSetSystem())
                //TODO change it to players with guids
                .Add(new FakeAssignCharactersToSpotsSystem())
                .Add(new CharacterSpotInitSetupSystem());
            
            _fixedUpdateSystems
                .Add(new MovementSystem())
                .Add(new RotationSystem());
        }

        private void AddInjections()
        {
            _systems.Inject(_gameSettings);
            _fixedUpdateSystems.Inject(_gameSettings);
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
