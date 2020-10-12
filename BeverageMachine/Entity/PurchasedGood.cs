namespace BeverageMachine.Entity
{
    public class PurchasedGood
    { 
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int DrinkId { get; set; }
        public Drink Drink { get; set; }
        public int ShoppingBasketViewModelId { get; set; }
        public ShoppingBasket ShoppingBasketViewModel { get; set; }
    }
}
