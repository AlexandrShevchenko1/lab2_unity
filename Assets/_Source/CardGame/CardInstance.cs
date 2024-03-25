namespace CardGame
{
    public class CardInstance
    {
        public readonly CardAsset Asset;

        public int LayoutId { get; set; }
        public LayoutStatus Status { get; set; }
        public int CardPosition { get; set; }

        public CardInstance(CardAsset asset)
        {
            Asset = asset;
        }
    }
}