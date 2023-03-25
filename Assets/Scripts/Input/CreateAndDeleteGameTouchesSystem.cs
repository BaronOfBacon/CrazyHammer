using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace CrazyHammer.Core.Input
{
    public class CreateAndDeleteGameTouchesSystem : IEcsPreInitSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<GameTouchComponent> _gameTouches = null;


        public void PreInit()
        {
            EnhancedTouchSupport.Enable();
            Touch.onFingerDown += CreateTouch;
            Touch.onFingerUp += DeleteTouch;
        }

        public void Destroy()
        {
            Touch.onFingerDown -= CreateTouch;
            Touch.onFingerUp -= DeleteTouch;
            EnhancedTouchSupport.Disable();
        }

        private void CreateTouch(Finger finger)
        {
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

        private void DeleteTouch(Finger finger)
        {
            foreach (var index in _gameTouches)
            {
                if (_gameTouches.Get1(index).ID != finger.index)
                    continue;
                _gameTouches.GetEntity(index).Destroy();
            }
        }
    }
}