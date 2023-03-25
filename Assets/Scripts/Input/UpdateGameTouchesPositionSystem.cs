using System.Linq;
using Leopotam.Ecs;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace CrazyHammer.Core.Input
{
    public class UpdateGameTouchesPositionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ProcessGameFieldInputComponent> _filter = null;
        private readonly EcsFilter<GameTouchComponent> _gameTouches = null;
        
        public void Run()
        {
            if (_filter.IsEmpty())
            {
                if(!_gameTouches.IsEmpty())
                    DeleteAllTouches();
                return;
            }
            
            if (Touch.activeTouches.Count == 0) return;

            foreach (var index in _gameTouches)
            {
                ref var gameTouch = ref _gameTouches.Get1(index);
                var fingerID = gameTouch.ID;
                var finger = Touch.activeFingers.First(finger => finger.index == fingerID);
                gameTouch.ScreenPosition = finger.currentTouch.screenPosition;
            }
        }
        
        private void DeleteAllTouches()
        {
            foreach (var index in _gameTouches)
            {
                _gameTouches.GetEntity(index).Destroy();
            }
        }
    }
}