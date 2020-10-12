using BeverageMachine.Entity;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace BeverageMachine.Services
{
    public static class SessionService
    {
        public enum SessionKey
        { 
            BASKET,
            RETURN_URL
        }

        public static void Set<T>(this ISession session, SessionKey key, T value)
        {
            session.SetString(key.ToString(), JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, SessionKey key)
        {
            var value = session.GetString(key.ToString());
            return value == null && value is T ? default : JsonSerializer.Deserialize<T>(value);
        }

        public static ShoppingBasket GetBasket(this ISession session)
        {
            ShoppingBasket basket = Get<ShoppingBasket>(session, SessionKey.BASKET);
            if (basket == null)
            {
                basket = new ShoppingBasket();
                Set<ShoppingBasket>(session, SessionKey.BASKET, basket);
            }
            return basket;
        }
    }
}
