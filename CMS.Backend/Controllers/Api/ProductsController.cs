/*
 * Ho va ten: Nguyen Thi Man Duy
 * Mssv: 2123110470
 * Version 2.1
 * Ngay thuc hien: 25/6/2026
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS.Data;

namespace CMS.Backend.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;


        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }





        // =====================================================
        // GET: api/products
        // =====================================================
        [HttpGet]
        public async Task<IActionResult> GetAll(
            int? categoryId,
            string? search,
            decimal? minPrice,
            decimal? maxPrice,
            string? sortBy,
            int page = 1,
            int pageSize = 12)
        {


            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();





            if (categoryId.HasValue)
            {
                query = query.Where(p =>
                    p.CategoryProductId == categoryId.Value);
            }





            if (!string.IsNullOrWhiteSpace(search))
            {
                var keyword = search.Trim();


                query = query.Where(p =>
                    p.Name.Contains(keyword)
                    ||
                    (
                        p.Description != null &&
                        p.Description.Contains(keyword)
                    )
                );
            }





            if (minPrice.HasValue)
            {
                query = query.Where(p =>
                    p.Price >= minPrice.Value);
            }




            if (maxPrice.HasValue)
            {
                query = query.Where(p =>
                    p.Price <= maxPrice.Value);
            }






            query = sortBy switch
            {

                "price_asc" =>
                    query.OrderBy(p => p.Price),


                "price_desc" =>
                    query.OrderByDescending(p => p.Price),


                "name_asc" =>
                    query.OrderBy(p => p.Name),


                "newest" =>
                    query.OrderByDescending(p => p.Id),


                _ =>
                    query.OrderByDescending(p => p.Id)

            };







            var totalCount =
                await query.CountAsync();







            var products =
                await query

                .Skip((page - 1) * pageSize)

                .Take(pageSize)

                .Select(p => new
                {

                    p.Id,

                    p.Name,

                    p.Description,

                    p.Price,

                    p.StockQuantity,

                    p.ImageUrl,

                    p.CategoryProductId,


                    CategoryName =
                        p.Category != null
                        ? p.Category.Name
                        : ""

                })

                .ToListAsync();






            return Ok(new
            {

                data = products,

                totalCount,

                page,

                pageSize,

                totalPages =
                    (int)Math.Ceiling(
                        (double)totalCount / pageSize)

            });

        }









        // =====================================================
        // BEST SELLERS
        // =====================================================
        [HttpGet("best-sellers")]
        public async Task<IActionResult> GetBestSellers(
            int limit = 8)
        {


            var bestSellers =
                await _context.OrderDetails


                .GroupBy(x => x.ProductId)


                .Select(g => new
                {

                    ProductId = g.Key,

                    TotalSold =
                        g.Sum(x => x.Quantity)

                })



                .OrderByDescending(x => x.TotalSold)

                .Take(limit)





                .Join(

                    _context.Products
                    .Include(p => p.Category),


                    x => x.ProductId,


                    p => p.Id,



                    (x, p) => new
                    {

                        p.Id,

                        p.Name,

                        p.Description,

                        p.Price,

                        p.StockQuantity,

                        p.ImageUrl,

                        p.CategoryProductId,


                        CategoryName =
                            p.Category != null
                            ? p.Category.Name
                            : "",



                        x.TotalSold

                    })



                .OrderByDescending(x => x.TotalSold)


                .ToListAsync();






            return Ok(bestSellers);

        }









        // =====================================================
        // GET DETAIL
        // =====================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {


            var product =
                await _context.Products

                .Include(p => p.Category)


                .Where(p => p.Id == id)


                .Select(p => new
                {

                    p.Id,

                    p.Name,

                    p.Description,

                    p.Price,

                    p.StockQuantity,

                    p.ImageUrl,

                    p.CategoryProductId,


                    CategoryName =
                        p.Category != null
                        ? p.Category.Name
                        : ""

                })


                .FirstOrDefaultAsync();





            if (product == null)
                return NotFound();







            var relatedProducts =
                await _context.Products


                .Where(p =>
                    p.CategoryProductId ==
                    product.CategoryProductId
                    &&
                    p.Id != id)



                .Take(4)



                .Select(p => new
                {

                    p.Id,

                    p.Name,

                    p.Price,

                    p.ImageUrl

                })


                .ToListAsync();







            return Ok(new
            {
                product,

                relatedProducts
            });

        }









        // =====================================================
        // CHECK STOCK
        // =====================================================
        [HttpGet("stock/{id}")]
        public async Task<IActionResult> GetStock(
            int id)
        {


            var product =
                await _context.Products


                .Where(p => p.Id == id)


                .Select(p => new
                {

                    p.Id,

                    p.StockQuantity

                })


                .FirstOrDefaultAsync();





            if (product == null)
                return NotFound();






            string status;



            if (product.StockQuantity <= 0)

                status = "Hết hàng";


            else if (product.StockQuantity <= 5)

                status = "Sắp hết";


            else

                status = "Còn nhiều";






            return Ok(new
            {

                product.StockQuantity,

                Status = status,

                LastChecked = DateTime.UtcNow

            });


        }



    // =====================================================
// HERO PRODUCTS
// =====================================================
[HttpGet("hero")]
        public async Task<IActionResult> GetHeroProducts(int limit = 3)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => !string.IsNullOrEmpty(p.ImageUrl))
                .OrderByDescending(p => p.Id)
                .Take(limit)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.ImageUrl,
                    CategoryName = p.Category != null ? p.Category.Name : ""
                })
                .ToListAsync();

            return Ok(products);
        }
    }
}