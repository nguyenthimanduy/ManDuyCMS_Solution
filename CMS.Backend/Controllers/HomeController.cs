/*
 * Ho va ten: Nguyen Thi Man Duy
 * Mssv: 2123110470
 * Version 2
 * Ngay thuc hien: 4/6/2026
 */
using Microsoft.AspNetCore.Authorization;
using CMS.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CMS.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Backend.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;



        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext context)
        {

            _logger = logger;

            _context = context;

        }






        public IActionResult Index()
        {


            // ==========================
            // THỐNG KÊ
            // ==========================


            ViewBag.TotalProduct =
                _context.Products.Count();



            ViewBag.TotalOrder =
                _context.Orders.Count();



            ViewBag.TotalCustomer =
                _context.Customers.Count();



            ViewBag.TotalPost =
                _context.Posts.Count();







            // ==========================
            // DOANH THU
            // ==========================


            var totalRevenue =
                _context.OrderDetails
                .Sum(x =>
                (decimal?)x.UnitPrice * x.Quantity)
                ?? 0;



            ViewBag.TotalRevenue = totalRevenue;









            // ==========================
            // 5 ĐƠN HÀNG MỚI NHẤT
            // ==========================


            var latestOrders =
                _context.Orders

                .Include(x => x.Customer)

                .OrderByDescending(x => x.OrderDate)

                .Take(5)

                .ToList();



            ViewBag.LatestOrders = latestOrders;









            // ==========================
            // 3 BÀI VIẾT MỚI NHẤT
            // ==========================


            var latestPosts =
                _context.Posts

                .Include(x => x.Category)

                .OrderByDescending(x => x.CreatedDate)

                .Take(3)

                .ToList();



            ViewBag.LatestPosts = latestPosts;







            return View();

        }






        public IActionResult Privacy()
        {
            return View();
        }





        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]

        public IActionResult Error()
        {

            return View(

                new ErrorViewModel

                {

                    RequestId =
                    Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier

                });

        }


    }

}