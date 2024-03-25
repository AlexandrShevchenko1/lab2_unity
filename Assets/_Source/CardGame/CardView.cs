using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class CardView : MonoBehaviour
    {
        public CardInstance CardInstance;
        private Image _image;
        
        public void PlayCard()
        {
            CardGame.Instance.MoveToSpecificLayout(CardInstance,
                CardInstance.Status == LayoutStatus.OnHand ? LayoutType.CenterLayout : LayoutType.DeleteLayout);
        }

        public void Rotate(bool up)
        {
            _image.sprite = up switch
            {
                true => CardInstance.Asset.shirtOn,
                false => CardInstance.Asset.shirtOff
            };
        }
        
        public void Construct(CardInstance instance, Image imageObj)
        {
            CardInstance = instance;
            _image = imageObj;
        }
    }
}