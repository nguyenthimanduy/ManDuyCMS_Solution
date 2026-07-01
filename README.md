💄 ManDuyCMS – Hệ thống Quản lý Nội dung & Thương mại Điện tử Mỹ phẩm

ManDuyCMS là hệ thống website thương mại điện tử kết hợp Quản lý nội dung (CMS), chuyên phục vụ hoạt động kinh doanh mỹ phẩm trực tuyến. Hệ thống được phát triển bằng ASP.NET Core 8 (Backend) và React 19 (Frontend), hỗ trợ quản lý sản phẩm, đơn hàng, bài viết, khách hàng và thanh toán trực tuyến.

📋 Mục lục
Tổng quan
Công nghệ sử dụng
Kiến trúc hệ thống
Tính năng chính
Cài đặt & Chạy dự án
Cấu trúc thư mục
API Endpoints
Tác giả
🌐 Tổng quan

ManDuyCMS bao gồm hai thành phần chính:

Thành phần	Mô tả
🖥️ Trang quản trị (Admin MVC)	Hệ thống quản trị được xây dựng bằng ASP.NET Core MVC và Razor Views, hỗ trợ quản lý mỹ phẩm, danh mục sản phẩm, bài viết, khách hàng, đơn hàng và tài khoản quản trị.
💄 Trang khách hàng (React SPA)	Giao diện mua sắm hiện đại dành cho người dùng với các chức năng xem sản phẩm, tìm kiếm, giỏ hàng, thanh toán, theo dõi đơn hàng và đọc bài viết tư vấn làm đẹp.

Hai thành phần trao đổi dữ liệu thông qua RESTful API, giúp tách biệt rõ ràng giữa Frontend và Backend.

🛠️ Công nghệ sử dụng
Backend
Công nghệ	Phiên bản	Mục đích
ASP.NET Core	8.0	Xây dựng Backend
Entity Framework Core	8.0	ORM
SQL Server	-	Hệ quản trị CSDL
Swagger	10.x	Tài liệu API
Cookie Authentication	-	Xác thực Admin
Frontend
Công nghệ	Phiên bản	Mục đích
React	19.x	Giao diện người dùng
React Router DOM	7.x	Điều hướng SPA
Axios	Latest	Gọi API
React Icons	5.x	Bộ biểu tượng
React Hot Toast	2.x	Thông báo
🏗️ Kiến trúc hệ thống
Client (Browser)
│
├── React SPA (Khách hàng)
│
├── ASP.NET Core MVC (Quản trị)
│
└── RESTful API
        │
        ▼
ASP.NET Core Backend
        │
Entity Framework Core
        │
SQL Server Database

Dự án được xây dựng theo mô hình 3 Layer Architecture

CMS.Frontend – ReactJS
CMS.Backend – ASP.NET Core MVC + Web API
CMS.Data – Entity Framework Core + SQL Server
✨ Tính năng chính
💄 Phía khách hàng
Hiển thị danh sách mỹ phẩm
Chi tiết sản phẩm
Tìm kiếm sản phẩm
Lọc theo danh mục
Giỏ hàng
Thanh toán
Thanh toán VNPay
Đăng ký
Đăng nhập
Quên mật khẩu qua Email
Quản lý thông tin cá nhân
Quản lý địa chỉ giao hàng
Lịch sử đơn hàng
Xem bài viết chia sẻ kiến thức làm đẹp
🖥️ Phía quản trị
Quản lý sản phẩm mỹ phẩm
Quản lý danh mục sản phẩm
Quản lý bài viết
Quản lý khách hàng
Quản lý đơn hàng
Quản lý tài khoản nhân viên
Quản lý hình ảnh sản phẩm
Quản lý banner
Quản lý danh mục bài viết
Thống kê dữ liệu
🚀 Cài đặt dự án
Yêu cầu
.NET 8 SDK
NodeJS 18+
SQL Server
Clone Project
git clone https://github.com/nguyenthimanduy/ManDuyCMS_Solution.git
Database

Cập nhật chuỗi kết nối trong

CMS.Backend/appsettings.json

Sau đó chạy

dotnet ef database update
Chạy Backend
cd CMS.Backend

dotnet run
Chạy Frontend
cd CMS.Frontend

npm install

npm start
📁 Cấu trúc dự án
CMS.Backend/
│
├── Controllers/
├── Views/
├── wwwroot/
├── Helpers/
├── Program.cs
└── appsettings.json

CMS.Data/
│
├── Entities/
├── Migrations/
└── ApplicationDbContext.cs

CMS.Frontend/
│
├── src/
│   ├── components/
│   ├── pages/
│   ├── styles/
│   └── api/
│
└── public/
📡 API

Một số API chính

Method	Endpoint
GET	/api/products
GET	/api/categories
GET	/api/posts
POST	/api/customerauth/login
POST	/api/customerauth/register
GET	/api/orders
POST	/api/orders

Tài liệu API được cung cấp thông qua Swagger UI.

👤 Tác giả

Man Duy

GitHub: https://github.com/nguyenthimanduy/ManDuyCMS_Solution.git

📄 License

Dự án được phát triển nhằm phục vụ đồ án tốt nghiệp, học tập và nghiên cứu về hệ thống quản lý nội dung (CMS) kết hợp website thương mại điện tử kinh doanh mỹ phẩm.
