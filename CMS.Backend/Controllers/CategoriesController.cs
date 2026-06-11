/*
 * Ho va ten: Nguyen Thi Man Duy
 * Mssv: 2123110470
 * Version 2
 * Ngay thuc hien: 4/6/2026
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using System.Threading.Tasks;
using System.Linq;

namespace CMS.Backend.Controllers
{
    // Cấu hình cứng đường dẫn phân biệt rõ ràng: api/Categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hàm HTTP GET lấy toàn bộ chuyên mục bài viết tin tức
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Lấy dữ liệu từ bảng chuyên mục tin tức
                var categories = await _context.Categories
                    .Select(c => new {
                        c.Id,
                        c.Name,
                        c.Description
                    })
                    .ToListAsync();

                return Ok(categories);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi kết nối cơ sở dữ liệu chuyên mục",
                    detail = ex.Message
                });
            }
        }
    }
}