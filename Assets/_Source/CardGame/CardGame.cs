using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace CardGame
{
    public class CardGame
    {
        private static CardGame _instance;
        public static CardGame Instance
        {
            get
            {
                return _instance ??= new CardGame();
            }
        }
        
        private readonly Dictionary<CardInstance, CardView> _cardMapper = new(); 
        private List<CardAsset> _existedCards;
        private int _capacity;
        public int HowManyInHand;
        
        
        private CardLayout _deletedLayout;
        public CardLayout CenterLayout;
        private List<CardLayout> _layouts = new();

        public void Init(List<CardLayout> layouts, List<CardAsset> assets, int capacity, List<CardLayout> other, int offset)
        {
            CenterLayout = other[0];
            _deletedLayout = other[1];
            
            _layouts = layouts;
            _existedCards = assets;
            _capacity = capacity;
            HowManyInHand = offset;
            StartGame();
        }

        private void StartGame()
        {
            foreach (var layout in _layouts)
            {
                foreach (var card in _existedCards)
                {
                    CreateCard(card, layout.Id);
                }
            }
        }

        private void CreateCard(CardAsset asset, int layoutNumber)
        {
            var instance = new CardInstance(asset);
            instance.LayoutId = layoutNumber;
            instance.CardPosition = _layouts[layoutNumber].Counter;
     
            CreateCardView(instance);
            MoveToLayout(instance, layoutNumber);

            _layouts[layoutNumber].Counter += 1;
        }

        private void CreateCardView(CardInstance instance)
        {
            GameObject newCardInstance = new GameObject();
            Image image = newCardInstance.AddComponent<Image>();
            newCardInstance.transform.SetParent(_layouts[instance.LayoutId].transform);
            Button button = newCardInstance.AddComponent<Button>();
       
            CardView view = newCardInstance.AddComponent<CardView>();
            _cardMapper[instance] = view;
            button.onClick.AddListener(view.PlayCard);

            view.Construct(instance, image);
        }
        
        private void MoveToLayout(CardInstance card, int layoutId)
        {
            card.LayoutId = layoutId;
            _cardMapper[card].transform.SetParent(_layouts[layoutId].transform);

            foreach (var layout in _layouts)
            {
                RecalculateLayout(layout.Id);
            }
        }

        public void StartTurn()
        {
            foreach (var layout in _layouts)
            {
                layout.FaceUp = true;
                
                var cards = GetCardsInLayout(layout.Id);
                
                for (int i = 0; i < _capacity; ++i)
                {
                    cards[i].CardInstance.Status = LayoutStatus.OnHand;
                }
            }
        }
        
        private void RecalculateLayout(int layoutId)
        {
            var games = GetCardsInLayout(layoutId);

            for (int i = 0; i < games.Count; ++i)
            {
                games[i].CardInstance.CardPosition = i;
            }
        }

        private void ShuffleLayout(int layoutId)
        {
            Random randomizer = new Random();
            var cards = _cardMapper.Where(x => x.Key.LayoutId == layoutId).Select(x => x.Key).ToList();
            
            HashSet<int> positions = new HashSet<int>();
            while (positions.Count != cards.Count)
            {
                positions.Add(randomizer.Next(0, cards.Count));
            }

            List<CardInstance> copy = new List<CardInstance>(cards);

            for (var i = 0; i < cards.Count; ++i)
            {
                copy[i] = cards[positions.ElementAt(i)];
            }
            cards = copy;
        }
        
        public void MoveToSpecificLayout(CardInstance card, LayoutType type)
        {
            int temp = card.LayoutId;
            switch (type)
            {
                case LayoutType.CenterLayout:
                    card.LayoutId = CenterLayout.Id;
                    card.Status = LayoutStatus.OnCenter;
                    _cardMapper[card].transform.SetParent(CenterLayout.transform);
                    break;
                case LayoutType.DeleteLayout:
                    card.LayoutId = _deletedLayout.Id;
                    card.Status = LayoutStatus.OnSbros;
                    _cardMapper[card].transform.SetParent(_deletedLayout.transform);
                    break;
            }
            
            foreach (var layout in _layouts)
            {
                RecalculateLayout(layout.Id);
            }
        }
        
        public List<CardView> GetCardsInLayout(int layoutId)
        {
            return _cardMapper.Where(x => x.Key.LayoutId == layoutId).Select(x => x.Value).ToList();
        }
    }
}