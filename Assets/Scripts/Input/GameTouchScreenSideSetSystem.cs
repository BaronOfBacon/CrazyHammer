using Leopotam.Ecs;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace CrazyHammer.Core.Input
{
    public class GameTouchScreenSideSetSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameTouchComponent, NewGameTouchComponent> _newGameTouches;
        private readonly EcsFilter<LeftScreenSideTouchComponent> _leftTouches;
        private readonly EcsFilter<RightScreenSideTouchComponent> _rightTouches;

        public void Run()
        {
            foreach (var index in _newGameTouches)
            {
                var entity = _newGameTouches.GetEntity(index);
                entity.Del<NewGameTouchComponent>();
                
                if (_leftTouches.IsEmpty())
                {
                    var startScreenPosition = _newGameTouches.Get1(index).StartScreenPosition;
                    if (startScreenPosition.x < Screen.width / 2f)
                    {
                        entity.Get<LeftScreenSideTouchComponent>();
                    }
                }

                if (_rightTouches.IsEmpty())
                {
                    var startScreenPosition = _newGameTouches.Get1(index).StartScreenPosition;
                    if (startScreenPosition.x > Screen.width / 2f)
                    {
                        entity.Get<RightScreenSideTouchComponent>();
                    }
                }
            }
        }
    }
}