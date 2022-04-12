using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSepet.Models
{
    public class Cart
    {
        
        //Sepet Listesi
        private Dictionary<int, CartItem> _myCart = new Dictionary<int, CartItem>();


        //Sepet
        public List<CartItem> myCart
        {
            get
            {
                return _myCart.Values.ToList();
            }
        }

        
        //Sepete Ekleme

        public void AddItem(CartItem cartItem)
        {
            if (_myCart.ContainsKey(cartItem.Id))
            {
                _myCart[cartItem.Id].Quantity += cartItem.Quantity;
                return;
            }
            _myCart.Add(cartItem.Id, cartItem);
        }

        //Sepete Güncelleme


    }
}