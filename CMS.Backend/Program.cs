using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using CMS.Data;
using System.Text.Json.Serialization;
using CMS.Backend.Helpers;
var builder = WebApplication.CreateBuilder(args);

// Register EmailService
builder.Services.AddSingleton<EmailService>();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// ==========================================
// CẤU HÌNH CORS CHO FRONTEND REACT
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddDbContext<CMS.Data.ApplicationDbContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================================
// CẤU HÌNH AUTHENTICATION BẰNG COOKIE
// ==========================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";           // Đường dẫn trang đăng nhập
        options.LogoutPath = "/Account/Logout";          // Đường dẫn đăng xuất
        options.AccessDeniedPath = "/Account/AccessDenied"; // Trang từ chối truy cập
        options.ExpireTimeSpan = TimeSpan.FromHours(8);  // Cookie tồn tại 8 giờ
        options.SlidingExpiration = true;                 // Tự động gia hạn khi còn hoạt động
        options.Cookie.HttpOnly = true;                   // Bảo mật: không truy cập được từ JS
        options.Cookie.Name = "ManDuyCMS.Auth";              // Tên cookie
    });

// ==========================================
// CẤU HÌNH SWAGGER
// ==========================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ManDuyCMS API v1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowFrontend");

app.UseRouting();

// THỨ TỰ QUAN TRỌNG: Authentication PHẢI đứng trước Authorization
app.UseAuthentication();
app.UseAuthorization();

// Seed CategoryProduct data programmatically if empty
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CMS.Data.ApplicationDbContext>();
        if (!context.CategoriesProduct.Any())
        {
            context.CategoriesProduct.AddRange(
				new CMS.Data.Entities.CategoriesProduct
				{
					Id = 1,
					Name = "Son",
					Description = "Các loại son môi"
				},
	new CMS.Data.Entities.CategoriesProduct
	{
		Id = 2,
		Name = "Phấn phủ",
		Description = "Các loại phấn phủ trang điểm"
	},
	new CMS.Data.Entities.CategoriesProduct
	{
		Id = 3,
		Name = "Tẩy trang",
		Description = "Sản phẩm tẩy trang cho da"
	}
			);
            context.SaveChanges();
        }

        // Seed tài khoản Admin mặc định nếu chưa có User nào
        if (!context.Users.Any())
        {
            context.Users.Add(new CMS.Data.Entities.User
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), // Mật khẩu đã được băm bằng BCrypt
                FullName = "Quản trị viên",
                Role = "Admin"
            });
            context.SaveChanges();
        }

        // Băm mật khẩu plaintext còn sót lại (User và Customer)
        var usersToHash = context.Users.Where(u => !u.PasswordHash.StartsWith("$2")).ToList();
        foreach (var u in usersToHash)
        {
            u.PasswordHash = BCrypt.Net.BCrypt.HashPassword(u.PasswordHash);
        }
        var customersToHash = context.Customers.Where(c => !c.PasswordHash.StartsWith("$2")).ToList();
        foreach (var c in customersToHash)
        {
            c.PasswordHash = BCrypt.Net.BCrypt.HashPassword(c.PasswordHash);
        }
        if (usersToHash.Any() || customersToHash.Any())
        {
            context.SaveChanges();
            Console.WriteLine($"[SEED] Đã băm mật khẩu: {usersToHash.Count} User, {customersToHash.Count} Customer");
        }
    }
    catch (Exception)
    {
        // Silent catch or logging
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
