using CrazyHammer.Core.Data;
using Leopotam.Ecs;

namespace CrazyHammer.Core
{
    public class FakeAssignCharactersToSpotsSystem : IEcsInitSystem
    {
        private GameSettings _gameSettings = null;
        
        public EcsFilter<CharacterSpot> _spots;
        public EcsFilter<PlayerTag> _playerTag;

        public void Init()
        {
            ref var playerEntity = ref _playerTag.GetEntity(0);

            foreach (var index in _spots)
            {
                var spot = _spots.Get1Ref(index);

                if (spot.Unref().Side != _gameSettings.playerFakeInitSide) continue;
                
                spot.Unref().CharacterEntity = playerEntity;
                return;
            }
        }
    }
}
