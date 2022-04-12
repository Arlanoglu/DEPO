using MVCSepet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSepet.Controllers
{
    public class HomeController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        public ActionResult Index()
        {
            var products = db.Products.Take(16).ToList();
            return View(products);
        }

        public ActionResult AddToCart(int id)
        {
            
            try
            {
                Product eklenecekUrun = db.Products.Find(id);
                Cart c = null;
                if (Session["scart"] == null)
                {
                    c = new Cart();
                }
                else
                {
                    c = Session["scart"] as Cart;
                }


                CartItem ci = new CartItem();
                ci.Id = eklenecekUrun.ProductID;
                ci.Name = eklenecekUrun.ProductName;
                ci.Price = eklenecekUrun.UnitPrice;

                c.AddItem(ci);
                Session["scart"] = c;
                TempData["Success"] = $"{eklenecekUrun.ProductName} sepete eklendi!";

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }


            return RedirectToAction("Index");
        }

        public ActionResult MyCart()
        {
            Cart cart = Session["scart"] as Cart;
            return View(cart);
        }

        public ActionResult CompleteCart()
        {
            Cart cart = Session["scart"] as Cart;
            foreach (var item in cart.myCart)
            {
                Product product = db.Products.Find(item.Id);
                product.UnitsInStock -= Convert.ToInt16(item.Quantity);
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            Order order = new Order();
            order.OrderID = 1000;
            order.OrderDate = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();
            Session.Remove("scart");
            return RedirectToAction("OrderResult",order);
        }

        public ActionResult OrderResult(Order order)
        {
            return View(order);
        }

    }
}