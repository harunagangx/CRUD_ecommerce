using BTL_LTWNC.Data;
using BTL_LTWNC.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace BTL_LTWNC.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("shop")]
        public async Task<IActionResult> getAllProduct(int cateID, string sortOrder)
        {
            ViewBag.cateList = new SelectList(_dbContext.tblProductCategory.ToList(), "iCategoryID", "sCategoryName");

            List<Product> productList = new List<Product>();

            var data = await _dbContext.tblProduct.ToListAsync();

            if (cateID == 0)
            {
                data = data;
            }
            else
            {
                data = data.Where(x => x.iProductCategoryID == cateID).ToList();
            }

            productList = data;

            switch (sortOrder)
            {
                case "ascending":
                    productList = productList.OrderBy(x => x.fPrice).ToList();
                    break;
                case "descending":
                    productList = productList.OrderByDescending(x => x.fPrice).ToList();
                    break;
                default:
                    productList = productList;
                    break;
            }

            return View(productList);
        }

        [HttpGet]
        [Route("productDetails")]
        public async Task<IActionResult> productDetails(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var product = await _dbContext.tblProduct.Where(x => x.iProductID == id).SingleOrDefaultAsync();

            SqlParameter cateID = new SqlParameter("@categoryID", product.iProductCategoryID);

            var relatedProduct = await _dbContext.tblProduct.FromSqlRaw("spGetRelatedProduct @categoryID", cateID).ToListAsync();

            ViewBag.relatedProduct = relatedProduct;

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult searchProduct(string search)
        {
            List<Product> listProduct = new List<Product>();

            var data = _dbContext.tblProduct.Where(x => x.sProductName.Contains(search)).ToList();

            listProduct = data;

            return View(listProduct);
        }
    }
}



