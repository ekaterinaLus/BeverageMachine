namespace BeverageMachine.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public ShoppingBasket Basket { get; set; }
        public decimal Amount { get; set; }
    }
}
