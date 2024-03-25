using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class GameSettings : MonoBehaviour
    {
        [SerializeField] private List<CardAsset> assets;
        [SerializeField] private int capacity;
        [SerializeField] private CardLayout centerLayout;
        [SerializeField] private CardLayout deletedLayout;
        [SerializeField] private List<CardLayout> initialLayouts;
        [SerializeField] private int offsetY;
        
        private void Start()
        {
            int i = 0;
            for (; i < initialLayouts.Count; ++i)
            {
                initialLayouts[i].Id = i;
            }
            centerLayout.Id = i++;
            centerLayout.type = LayoutType.CenterLayout;
            
            deletedLayout.Id = i;
            deletedLayout.type = LayoutType.DeleteLayout;

            List<CardLayout> initializer = new List<CardLayout>();
            initializer.Add(centerLayout);
            initializer.Add(deletedLayout);
            
            CardGame.Instance.Init(initialLayouts, assets, capacity, initializer, offsetY);
        }
        
        public void StartTurn()
        {
           CardGame.Instance.StartTurn();
        }
    }
}