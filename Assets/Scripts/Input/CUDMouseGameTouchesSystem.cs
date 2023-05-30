using CrazyHammer.Core.Input;
using Leopotam.Ecs;
using UnityEngine.InputSystem;

namespace CrazyHammer.Core
{
    public class CUDMouseGameTouchesSystem: IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<MouseTouchComponent> _mouseTouches = null;
        private readonly EcsFilter<GameIsRunningFlag> _gameIsRunning = null;

        public void Run()
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
                DeleteMouseTouch();

            if (_gameIsRunning.IsEmpty()) return;
                
            if (Mouse.current.leftButton.wasPressedThisFrame)
                CreateMouseTouch();
            
            if (Mouse.current.leftButton.isPressed)
                UpdateMouseTouch();
        }

        private void CreateMouseTouch()
        {
            var entity = _world.NewEntity();
            ref var newTouch = ref entity.Get<GameTouchComponent>();
            newTouch.ID = -1;
            newTouch.StartScreenPosition = Mouse.current.position.ReadValue();
            newTouch.ScreenPosition = Mouse.current.position.ReadValue();
            entity.Get<NewGameTouchComponent>();
            entity.Get<MouseTouchComponent>();
        }

        private void UpdateMouseTouch()
        {
            ref var touch = ref _mouseTouches.GetEntity(0).Get<GameTouchComponent>();
            touch.ScreenPosition = Mouse.current.position.ReadValue();
        }

        private void DeleteMouseTouch()
        {
            _mouseTouches.GetEntity(0).Destroy();
        }
    }
}
