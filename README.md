Nàng Quýt - Website Bán Mỹ Phẩm

Sinh viên: Nguyễn Thị Mẫn Duy | MSSV: 2123110470 | Phiên bản: 3.0
1. Giới thiệu
Dự án website bán mỹ phẩm online hỗ trợ khách hàng xem, tìm kiếm, lọc sản phẩm và đặt
hàng trực tuyến.
2. Công nghệ sử dụng
Backend: ASP.NET Core Web API, C#, Entity Framework Core, SQL Server, REST
API.
Frontend: ReactJS, JavaScript, Axios, React Router, Bootstrap, CSS.
Database: SQL Server.
3. Cấu trúc thư mục

BeautyShop_Project
├── CMS.Backend
├── CMS.Data
└── cms.frontend

4. Yêu cầu cài đặt
Backend: .NET 8 SDK.
Frontend: NodeJS.
Database: SQL Server & SQL Server Management Studio.
5. Cấu hình & Khởi tạo Database
Chỉnh sửa CMS.Backend/appsettings.json với ConnectionStrings phù hợp. Chạy
lệnh:
Add-Migration InitialCreate
Update-Database

6. Hướng dẫn chạy
Backend:
Mở CMS.Backend.sln bằng Visual Studio, nhấn F5. Kiểm tra tại: https://localhost:
7041/swagger
Frontend:

cd cms.frontend
npm install
npm start
