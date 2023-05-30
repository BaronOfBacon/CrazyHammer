using System.Linq;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace CrazyHammer.Core.Input
{
    public class CUDGameTouchesSystem : IEcsPreInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<GameTouchComponent>.Exclude<MouseTouchComponent> _gameTouches = null;
        private readonly EcsFilter<GameIsRunningFlag> _gameIsRunning;
        
        public void PreInit()
        {
            EnhancedTouchSupport.Enable();
            Touch.onFingerDown += CreateTouch;
            Touch.onFingerUp += DeleteTouch;
        }

        public void Run()
        {
            if (Touch.activeTouches.Count > 0)
                UpdateTouches();
        }

        private void CreateTouch(Finger finger)
        {
            if (_gameIsRunning.IsEmpty()) return;
            
            foreach (var index in _gameTouches)
            {
                if (_gameTouches.Get1(index).ID == finger.index)
                {
                    Debug.LogError($"Touch with id [{finger.index}] is already exists!");
                }
            }

            var entity = _world.NewEntity();
            ref var newTouch = ref entity.Get<GameTouchComponent>();
            newTouch.ID = finger.index;
            newTouch.StartScreenPosition = finger.currentTouch.startScreenPosition;
            newTouch.ScreenPosition = finger.currentTouch.startScreenPosition;
            entity.Get<NewGameTouchComponent>();
        }
        
        private void UpdateTouches()
        {
            if (_gameIsRunning.IsEmpty()) return;
            
            foreach (var index in _gameTouches)
            {
                ref var gameTouch = ref _gameTouches.Get1(index);
                var fingerID = gameTouch.ID;
                var finger = Touch.activeFingers.First(finger => finger.index == fingerID);
                gameTouch.ScreenPosition = finger.currentTouch.screenPosition;
            } 
        }

        private void DeleteTouch(Finger finger)
        {
            foreach (var index in _gameTouches)
            {
                if (_gameTouches.Get1(index).ID != finger.index)
                    continue;
                _gameTouches.GetEntity(index).Destroy();
            }
        }

        public void Destroy()
        {
            Touch.onFingerDown -= CreateTouch;
            Touch.onFingerUp -= DeleteTouch;
            EnhancedTouchSupport.Disable();
        }
    }
}