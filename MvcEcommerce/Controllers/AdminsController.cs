using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Data;
using MvcEcommerce.Models;
using AddAdmins;
using System.Web.Security;


namespace MvcEcommerce.Controllers
{
    public class AdminsController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            AdminsManager mgr = new AdminsManager(Properties.Settings.Default.Constr);
            var adminUser = mgr.GetAdmin(username, password);
            if(adminUser == null)
            {
                return View();
            }
            FormsAuthentication.SetAuthCookie(adminUser.UserName, true);
            return RedirectToAction("UploadNew");
        }
        [Authorize]
        public ActionResult UploadNew()
        {
            ProductRepository repo = new ProductRepository(Properties.Settings.Default.Constr);
            IndexViewModel vm = new IndexViewModel();
            vm.Categories = repo.GetAllCategories();
            
            return View(vm.Categories);
        }
        [HttpPost]
        public ActionResult UploadNew(string name,decimal price,string shortDescription,string longDescription,int categoryid,HttpPostedFileBase[] images)
        {
            Product product = new Product();
            product.Name = name;
            product.Price = price;
            product.ShortDescription = shortDescription;
            product.LongDescription = longDescription;
            product.Categoryid = categoryid;

            ProductRepository repo = new ProductRepository(Properties.Settings.Default.Constr);
            repo.AddProduct(product);

           IEnumerable<Image> fileimages = images.Where(i 
             => i != null).Select(i =>
            {   Guid g = Guid.NewGuid();
                string fileName = g + ".jpg";
                i.SaveAs(Server.MapPath("~/Uploads/" + fileName));
                Image img = new Image { FileName = fileName,ProductId = product.Id };
                return img;
            });

           repo.AddImages(fileimages);

           return RedirectToAction("ShopHomePage", "Home");
        }

    }
}
