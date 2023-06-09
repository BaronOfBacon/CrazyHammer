using Sirenix.OdinInspector;
using UnityEngine;

namespace CrazyHammer.Core.Data
{
    [CreateAssetMenu(menuName = "Data/Game settings", fileName = "GameSettings"), InlineEditor()]
    public class GameSettings : SerializedScriptableObject
    {
        /*[SerializeField] 
        public BodySettings BodySettings;
        [SerializeField]
        public HandsSettings HandsSettings;*/
        [SerializeField]
        public Vector2Int TargetScreenRatio = new Vector2Int(1920,1080);
        
        [Space] 
        
        [Title("Test"), ShowInInspector] 
        public SpotSide PlayerFakeInitSide;
    }
}
