using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.Data;

namespace MvcEcommerce.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Category> Categories {get;set;}
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public Image Image { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public int CategoryId { get; set; }
    }
}