using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data
{
    public class ProductRepository
    {
        private string _connectionString;
        public ProductRepository(string connection)
        {
            _connectionString = connection;
        }
        public IEnumerable<Product> GetProductsByCategory(int? categoryId)
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                var loadOptions = new System.Data.Linq.DataLoadOptions();
                loadOptions.LoadWith<Product>(p => p.Images);
                dc.LoadOptions = loadOptions;
                return dc.Products.Where(p => p.Categoryid == categoryId).ToList();
            }
        }
        public Product GetProduct(int id)
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                var loadOptions = new System.Data.Linq.DataLoadOptions();
                loadOptions.LoadWith<Product>(p => p.Images);
                loadOptions.LoadWith<Product>(p => p.Category);
                dc.LoadOptions = loadOptions;
                return dc.Products.First(p => p.Id == id);
            }
        }
        public Image GetFirstImageByProduct(int productId)
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                return dc.Images.Where(i => i.ProductId == productId).FirstOrDefault();
            }
        }
        public IEnumerable<Image> GetAllImagesByProduct(int productId)
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                return dc.Images.Where(i => i.ProductId == productId).ToList();
            }
        }
        public IEnumerable<Image> GetAllImages()
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                return dc.Images.ToList();
            }
        }
        public IEnumerable<Category> GetAllCategories()
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                return dc.Categories.ToList();
            }
        }
        public void AddCategory(Category category)
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                dc.Categories.InsertOnSubmit(category);
                dc.SubmitChanges();
            }
        }
        public void AddProduct(Product product)
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                dc.Products.InsertOnSubmit(product);
                dc.SubmitChanges();
            }
        }
        public void AddImages(IEnumerable<Image> images)
        {
            using (var dc = new EcommerceDataContext(_connectionString))
            {
                dc.Images.InsertAllOnSubmit(images);
                dc.SubmitChanges();
            }
        }
    }
}
