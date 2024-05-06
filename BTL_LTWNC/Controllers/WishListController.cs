using BTL_LTWNC.Data;
using BTL_LTWNC.Helper;
using BTL_LTWNC.Models.EF;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTWNC.Controllers
{
    public class WishListController : Controller
    {
        private readonly AppDbContext _dbContext;

        public WishListController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<WishListItem> Wishlist
        {
            get
            {
                var data = HttpContext.Session.Get<List<WishListItem>>("Wishlist");

                if (data == null)
                {
                    data = new List<WishListItem>();
                }

                return data;
            }
        }

        public IActionResult Index()
        {
            return View(Wishlist);
        }

        [HttpPost]
        public JsonResult AddToWishList(int id)
        {
            var myWishList = Wishlist;

            var item = myWishList.SingleOrDefault(x => x.iProductID == id);

            var code = new { Success = false, code = -1 };

            if (item == null)
            {
                var product = _dbContext.tblProduct.SingleOrDefault(x => x.iProductID == id);

                item = new WishListItem
                {
                    iProductID = id,
                    sProductName = product.sProductName,
                    sImageUrl = product.sImageUrl,
                    fPrice = product.fPrice
                };

                myWishList.Add(item);
            }

            HttpContext.Session.Set("Wishlist", myWishList);
            code = new { Success = true, code = 1 };

            return Json(code);
        }

        [HttpPost]
        public ActionResult RemoveFormWishList(int id)
        {
            var myWishList = Wishlist;

            var code = new { Success = false, code = -1 };

            if (myWishList != null)
            {
                var item = myWishList.FirstOrDefault(x => x.iProductID == id);

                if (item != null)
                {
                    myWishList.Remove(item);

                    code = new { Success = true, code = 1 };
                }
            }

            HttpContext.Session.Set("Wishlist", myWishList);


            return Json(code);
        }

        [HttpGet]
        public JsonResult showCountWishList()
        {
            var myWishList = Wishlist;

            if (myWishList != null && myWishList.Any())
            {
                return Json(new { wishListQuantity = myWishList.Count });
            }

            return Json(new { wishListQuantity = 0 });
        }
    }
}
