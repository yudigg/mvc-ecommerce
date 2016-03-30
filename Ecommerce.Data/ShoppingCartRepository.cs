using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data
{
    public class ShoppingCartRepository
    {
        private string _connectionString;
        public ShoppingCartRepository(string connection)
        {
            _connectionString = connection;
        }
        ///needs more work
        public List<CartItem>GetCartItemsByShoppingCartId(int id)
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                DataLoadOptions options = new System.Data.Linq.DataLoadOptions();
                options.LoadWith<CartItem>(cartItem => cartItem.Product);
                options.LoadWith<Product>(product => product.Images);
                dc.LoadOptions = options;
                return dc.CartItems.Where(c => c.ShoppingCartId == id).ToList();
            }
        }
        public int GetTotalQuantityByShoppingCartId(int id)
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                DataLoadOptions options = new System.Data.Linq.DataLoadOptions();
                options.LoadWith<CartItem>(cartItem => cartItem.Product);
                options.LoadWith<Product>(product => product.Images);
                dc.LoadOptions = options;
                return dc.CartItems.AsEnumerable().Sum(c => c.Quantity);
            }
        }
     
        public void AddNewShopCart(ShoppingCart s)
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                dc.ShoppingCarts.InsertOnSubmit(s);
                dc.SubmitChanges();
            }
        }
        public void AddCartItems(CartItem c)
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                dc.CartItems.InsertOnSubmit(c);
                dc.SubmitChanges();
            }
        }
    }
}
