using CrazyHammer.Core.Data;
using CrazyHammer.Core.Input;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif
using UnityEngine;
using Voody.UniLeo;

namespace CrazyHammer.Core
{
    public class ECSGameStartup : SerializedMonoBehaviour
    {
        [OdinSerialize] 
        private GameSettings _gameSettings;

        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedUpdateSystems;
        private Vector2 _screenBasedTouchScaleRatio;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            _screenBasedTouchScaleRatio.x = (float)Screen.width / _gameSettings.TargetScreenRatio.x;
            _screenBasedTouchScaleRatio.y = (float)Screen.height / _gameSettings.TargetScreenRatio.y;
        }

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
                //inspector/debug
                .Add(new CUDMouseGameTouchesSystem())
                #endif
                .Add(new GameTouchScreenSideSetSystem())
                //TODO change it to players with guids
                .Add(new FakeAssignCharactersToSpotsSystem())
                .Add(new CharacterSpotInitSetupSystem());

            _fixedUpdateSystems
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                .Add(new PlayerMovementInputApplySystem())
                .Add(new HandsInputApplySystem());
        }

        private void AddInjections()
        {
            _systems.Inject(_gameSettings);
            _systems.Inject(_screenBasedTouchScaleRatio);
            _fixedUpdateSystems.Inject(_gameSettings);
            _fixedUpdateSystems.Inject(_screenBasedTouchScaleRatio);
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
