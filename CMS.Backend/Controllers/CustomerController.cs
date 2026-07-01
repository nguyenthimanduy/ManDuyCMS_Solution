/*
 * Ho va ten: Nguyen Thi Man Duy
 * Mssv: 2123110470
 * Version 2.1
 * Ngay thuc hien: 25/6/2026
 */
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Data.Entities;

namespace CMS.Backend.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // DANH SÁCH KHÁCH HÀNG (INDEX)
        // ==========================================
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }

        // ==========================================
        // 1. CHỨC NĂNG THÊM MỚI (CREATE)
        // ==========================================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            // Loại bỏ kiểm tra navigation property
            ModelState.Remove("Orders");

            if (ModelState.IsValid)
            {
                // Băm mật khẩu bằng BCrypt trước khi lưu
                customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.PasswordHash);
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // ==========================================
        // 2. CHỨC NĂNG SỬA (EDIT)
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.Id) return NotFound();

            // Loại bỏ kiểm tra navigation property
            ModelState.Remove("Orders");

            if (ModelState.IsValid)
            {
                // Băm mật khẩu mới bằng BCrypt nếu có thay đổi
                if (!string.IsNullOrWhiteSpace(customer.PasswordHash))
                {
                    customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.PasswordHash);
                }
                else
                {
                    // Giữ nguyên mật khẩu cũ nếu không nhập mật khẩu mới
                    var existing = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                    if (existing != null) customer.PasswordHash = existing.PasswordHash;
                }
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // ==========================================
        // 3. CHỨC NĂNG XÓA (DELETE)
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}