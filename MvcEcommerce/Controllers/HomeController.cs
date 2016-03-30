using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcEcommerce.Models;
using Ecommerce.Data;

namespace MvcEcommerce.Controllers
{
    public class HomeController : Controller
    {
   
        public ActionResult ShopHomePage(int? catId)
        {
            ProductRepository repo = new ProductRepository(Properties.Settings.Default.Constr);
            IndexViewModel vm = new IndexViewModel();
            vm.Categories = repo.GetAllCategories();
            if (catId == null)
            {
                int firstCatId = vm.Categories.First().Id;
                vm.Products = repo.GetProductsByCategory(firstCatId);
                vm.Images = repo.GetAllImages();
                vm.CategoryId = firstCatId;
            }
            else
            {
                vm.Products = repo.GetProductsByCategory(catId);
                vm.Categories = repo.GetAllCategories();
                vm.CategoryId = catId.Value;
            }
        
            return View(vm);
        }
      
       
        public ActionResult ShopItem(int pid)
        {
            ProductRepository repo = new ProductRepository(Properties.Settings.Default.Constr);
            IndexViewModel vm = new IndexViewModel();
            vm.Product = repo.GetProduct(pid);
            vm.Categories = repo.GetAllCategories();
            vm.Images = repo.GetAllImages();
            vm.CategoryId = vm.Product.Category.Id;
            return View(vm);
        }
        [HttpPost]
        public void CartIndex(int pid,int quantity)
        {
            if(quantity == 0)
            {
                return;
            }
            ShoppingCartRepository cartrepo = new ShoppingCartRepository(Properties.Settings.Default.Constr);
            int idValue = 0;
            if (Request.Cookies["cartid"] != null && )
            {
                idValue = int.Parse(Request.Cookies["cartid"].Value);
            }
            else 
            {

                ShoppingCart s = new ShoppingCart();
                s.Date = DateTime.Now;
                cartrepo.AddNewShopCart(s);
                idValue = s.Id;
                HttpCookie cookie = new HttpCookie("cartid", idValue.ToString());
                Response.Cookies.Add(cookie);
                 idValue = int.Parse(Request.Cookies["cartid"].Value);
            }

            CartItem c = new CartItem { ProductId = pid, ShoppingCartId = idValue, Quantity = quantity };
            cartrepo.AddCartItems(c);          
        }
        public ActionResult ViewCart()
        {
            List<CartItem> items = new List<CartItem>(); 
            if (Request.Cookies["cartid"] != null)
            {
               int idValue = int.Parse(Request.Cookies["cartid"].Value);
                 ShoppingCartRepository cartrepo = new ShoppingCartRepository(Properties.Settings.Default.Constr);
              items = cartrepo.GetCartItemsByShoppingCartId(idValue);
            }
           
            return View(items);
        }
        public ActionResult CartQuantity(int cartid)
        {
            ShoppingCartRepository cartrepo = new ShoppingCartRepository(Properties.Settings.Default.Constr);
           var qty = cartrepo.GetTotalQuantityByShoppingCartId(cartid);
           return Json(qty,JsonRequestBehavior.AllowGet);
        }
    }
}
