using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu]
    public class CardAsset : ScriptableObject
    {
        [SerializeField] private string cardName;

        [SerializeField] internal Sprite shirtOn;
        [SerializeField] internal Sprite shirtOff;
    }
}
