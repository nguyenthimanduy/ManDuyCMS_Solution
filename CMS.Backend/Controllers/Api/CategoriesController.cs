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
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.CategoriesProduct
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Description,
                    c.ImageUrl,
                    ProductCount = c.Products != null ? c.Products.Count : 0
                })
                .ToListAsync();

            return Ok(categories);
        }
    }
}
