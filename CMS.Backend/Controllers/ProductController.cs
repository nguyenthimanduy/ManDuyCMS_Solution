/*
 * Ho va ten: Nguyen Thi Man Duy
 * Mssv: 2123110470
 * Version 2.1
 * Ngay thuc hien: 25/6/2026
 */
using CMS.Data;
using CMS.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Backend.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;


        public ProductController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // ===============================
        // INDEX
        // ===============================
        public async Task<IActionResult> Index(
            int? categoryId,
            int page = 1)
        {

            int pageSize = 5;


            var query = _context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .AsQueryable();



            if (categoryId.HasValue)
            {
                query = query.Where(x =>
                    x.CategoryProductId == categoryId.Value);
            }



            // tổng số sản phẩm
            int totalItems = await query.CountAsync();



            // phân trang
            var products = await query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();





            // dữ liệu cho phân trang

            ViewBag.CurrentPage = page;


            ViewBag.TotalPages =
                (int)Math.Ceiling(
                    totalItems / (double)pageSize
                );


            ViewBag.CategoryId = categoryId;





            // danh mục

            ViewBag.Categories =
                new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                    await _context.CategoriesProduct.ToListAsync(),
                    "Id",
                    "Name",
                    categoryId
                );



            return View(products);

        }        // ===============================
        // CREATE GET
        // ===============================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadCategory();
            return View();
        }





        // ===============================
        // CREATE POST
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Product product,
            IFormFile? uploadImage)
        {

            ModelState.Remove("Category");


            if (ModelState.IsValid)
            {

                if (uploadImage != null &&
                    uploadImage.Length > 0)
                {
                    product.ImageUrl =
                        await UploadImage(uploadImage);
                }



                _context.Products.Add(product);

                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }



            await LoadCategory(product.CategoryProductId);


            return View(product);
        }






        // ===============================
        // EDIT GET
        // ===============================
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
                return NotFound();



            var product =
                await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);



            if (product == null)
                return NotFound();



            await LoadCategory(product.CategoryProductId);


            return View(product);
        }






        // ===============================
        // EDIT POST
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Product product,
            IFormFile? uploadImage)
        {

            if (id != product.Id)
                return NotFound();



            ModelState.Remove("Category");



            if (ModelState.IsValid)
            {

                var old =
                    await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == id);



                if (old == null)
                    return NotFound();




                old.Name = product.Name;
                old.Description = product.Description;
                old.Price = product.Price;
                old.StockQuantity = product.StockQuantity;
                old.CategoryProductId =
                    product.CategoryProductId;



                if (uploadImage != null &&
                   uploadImage.Length > 0)
                {

                    old.ImageUrl =
                        await UploadImage(uploadImage);

                }




                await _context.SaveChangesAsync();



                return RedirectToAction(nameof(Index));
            }



            await LoadCategory(product.CategoryProductId);


            return View(product);
        }






        // ===============================
        // DELETE GET
        // ===============================
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
                return NotFound();



            var product =
                await _context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);



            if (product == null)
                return NotFound();



            return View(product);
        }





        // ===============================
        // DELETE POST
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var product =
                await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);



            if (product != null)
            {

                _context.Products.Remove(product);

                await _context.SaveChangesAsync();

            }



            return RedirectToAction(nameof(Index));
        }







        // ===============================
        // UPLOAD IMAGE
        // ===============================
        private async Task<string> UploadImage(
            IFormFile file)
        {

            var folder =
                Path.Combine(
                    _environment.WebRootPath,
                    "images",
                    "products"
                );



            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }



            var fileName =
                Guid.NewGuid()
                + Path.GetExtension(file.FileName);



            var path =
                Path.Combine(
                    folder,
                    fileName
                );



            using (var stream =
                new FileStream(
                    path,
                    FileMode.Create))
            {

                await file.CopyToAsync(stream);

            }



            return "/images/products/" + fileName;
        }







        // ===============================
        // LOAD CATEGORY
        // ===============================
        private async Task LoadCategory(
            int? selected = null)
        {

            ViewBag.CategoriesProduct =
                new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                    await _context.CategoriesProduct.ToListAsync(),
                    "Id",
                    "Name",
                    selected
                );

        }

    }
}