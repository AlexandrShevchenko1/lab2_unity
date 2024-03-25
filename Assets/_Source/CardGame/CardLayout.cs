using System;
using UnityEngine;

namespace CardGame
{
    public class CardLayout : MonoBehaviour
    {
        [SerializeField] private Vector2 offsetInLine;
        public LayoutType type;
        
        internal int Id { get; set; }
        internal int Counter { get; set; }
        internal bool FaceUp { get; set; }

        internal void Update()
        {
            var cardsInLayout = CardGame.Instance.GetCardsInLayout(Id);
            
            foreach (var card in cardsInLayout)
            {
                try
                {
                    Transform cardTransform = card.GetComponent<Transform>();
                    
                    switch (card.CardInstance.Status)
                    {
                        case LayoutStatus.OnDeck:
                        {
                            FaceUp = false;
                            cardTransform.localPosition = new Vector2(card.CardInstance.CardPosition * offsetInLine.x, 0);
                            break;
                        }
                        case LayoutStatus.OnHand:
                        {
                            FaceUp = true;
                            cardTransform.localPosition = new Vector2(card.CardInstance.CardPosition  * offsetInLine.x, CardGame.Instance.HowManyInHand);
                            break;
                        }
                        case LayoutStatus.OnCenter:
                            FaceUp = true;
                            cardTransform.position = CardGame.Instance.CenterLayout.transform.position;
                            break;
                        case LayoutStatus.OnSbros:
                            FaceUp = false;
                            cardTransform.localPosition = new Vector2(0, card.CardInstance.CardPosition * CardGame.Instance.HowManyInHand);
                            break;
                    }
                    card.Rotate(FaceUp);
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
        }

    }
}