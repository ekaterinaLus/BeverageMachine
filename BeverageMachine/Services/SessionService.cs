using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BeverageMachine.ViewModel;
using Microsoft.AspNetCore.Http;

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

        public static ShoppingBasketViewModel GetBasket(this ISession session)
        {
            ShoppingBasketViewModel basket = Get<ShoppingBasketViewModel>(session, SessionKey.BASKET);
            if (basket == null)
            {
                basket = new ShoppingBasketViewModel();
                Set<ShoppingBasketViewModel>(session, SessionKey.BASKET, basket);
            }
            return basket;
        }
    }
}
