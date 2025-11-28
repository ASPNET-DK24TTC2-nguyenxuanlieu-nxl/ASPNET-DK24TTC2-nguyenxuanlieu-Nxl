# 🍎 Website Bán Hàng Apple - ASP.NET MVC

Website thương mại điện tử bán sản phẩm Apple được xây dựng bằng ASP.NET Core MVC với đầy đủ các chức năng quản lý sản phẩm, giỏ hàng, đơn hàng và tài khoản người dùng.

## ✨ Tính Năng Chính

### 🛒 Chức Năng Bán Hàng & Thương Mại Điện Tử

#### 1. Trình bày Sản phẩm (Product Display)
- ✅ Hiển thị chi tiết sản phẩm với hình ảnh chất lượng cao
- ✅ Thông số kỹ thuật chi tiết
- ✅ Phân loại sản phẩm rõ ràng theo danh mục:
  - iPhone
  - Mac
  - iPad
  - Watch
  - Phụ kiện

#### 2. Tùy chỉnh và Cấu hình sản phẩm (Customization & Configuration)
- ✅ Cho phép khách hàng tùy chỉnh cấu hình sản phẩm:
  - Chọn dung lượng bộ nhớ (256GB, 512GB, 1TB)
  - Chọn RAM (8GB, 16GB, 32GB)
  - Chọn CPU/SSD cho Mac
  - Chọn màu sắc
- ✅ Hiển thị giá điều chỉnh theo cấu hình đã chọn

#### 3. Giỏ hàng và Thanh toán (Cart & Checkout)
- ✅ Thêm/xóa sản phẩm vào Giỏ hàng
- ✅ Cập nhật số lượng sản phẩm
- ✅ Hiển thị tổng giá trị đơn hàng
- ✅ Quy trình thanh toán an toàn
- ✅ Hỗ trợ nhiều hình thức thanh toán:
  - Thanh toán khi nhận hàng (COD)
  - Trả góp hàng tháng (0% lãi suất)

#### 4. Tra cứu Trạng thái Đơn hàng (Order Status Tracking)
- ✅ Kiểm tra trạng thái đơn hàng:
  - Đã đặt hàng
  - Đang vận chuyển
  - Đã giao
- ✅ Xem lịch sử đơn hàng
- ✅ Xem chi tiết từng đơn hàng

### 🧑‍💻 Chức Năng Hỗ trợ Khách hàng & Cá nhân hóa

#### 1. Tạo và Quản lý Tài khoản (Account Management)
- ✅ Đăng ký tài khoản mới
- ✅ Đăng nhập/Đăng xuất
- ✅ Quản lý thông tin cá nhân
- ✅ Xem lịch sử mua hàng
- ✅ Quản lý đơn hàng của tôi

### ⚙️ Chức Năng Quản trị & Vận hành

#### 1. Quản lý Tồn kho và Giá cả (Admin Panel)
- ✅ Dashboard tổng quan:
  - Tổng số sản phẩm
  - Tổng số đơn hàng
  - Doanh thu
  - Đơn hàng đang chờ xử lý
  - Sản phẩm sắp hết hàng
- ✅ Quản lý sản phẩm (CRUD):
  - Thêm sản phẩm mới
  - Sửa thông tin sản phẩm
  - Xóa sản phẩm
  - Cập nhật tồn kho
  - Cập nhật giá
- ✅ Quản lý đơn hàng:
  - Xem danh sách đơn hàng
  - Cập nhật trạng thái đơn hàng
  - Xem chi tiết đơn hàng
- ✅ Quản lý cấu hình sản phẩm tùy chỉnh

## 🛠️ Công Nghệ Sử Dụng

### Backend
- **Framework**: ASP.NET Core 9.0 MVC
- **Database**: Microsoft SQL Server (LocalDB)
- **Authentication**: ASP.NET Core Identity
- **ORM**: Entity Framework Core 9.0

### Frontend
- **UI Framework**: Bootstrap 5
- **Icons**: Font Awesome 6.4
- **CSS**: Custom Apple-themed design
- **JavaScript**: jQuery, Bootstrap JS

### Packages
- `Microsoft.EntityFrameworkCore.SqlServer` (9.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (9.0.0)
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (9.0.0)
- `Microsoft.AspNetCore.Identity.UI` (9.0.0)
- `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` (9.0.0)

## 📁 Cấu Trúc Dự Án

```
Shopbanhangapple/
├── Controllers/
│   ├── HomeController.cs          # Trang chủ
│   ├── ProductsController.cs      # Quản lý sản phẩm (User)
│   ├── CartController.cs          # Giỏ hàng
│   ├── OrdersController.cs        # Đơn hàng
│   └── Admin/
│       ├── DashboardController.cs # Dashboard admin
│       ├── ProductsController.cs  # Quản lý sản phẩm (Admin)
│       └── OrdersController.cs    # Quản lý đơn hàng (Admin)
├── Models/
│   ├── Product.cs                 # Model sản phẩm
│   ├── Category.cs                # Model danh mục
│   ├── ProductConfiguration.cs    # Model cấu hình sản phẩm
│   ├── Order.cs                   # Model đơn hàng
│   ├── OrderDetail.cs             # Model chi tiết đơn hàng
│   └── CartItem.cs                # Model giỏ hàng
├── Data/
│   ├── ApplicationDbContext.cs    # Database context
│   └── SeedData.cs                # Dữ liệu mẫu
├── Views/
│   ├── Home/                      # Views trang chủ
│   ├── Products/                  # Views sản phẩm
│   ├── Cart/                      # Views giỏ hàng
│   ├── Orders/                    # Views đơn hàng
│   └── Shared/
│       └── _Layout.cshtml         # Layout chung
└── wwwroot/
    └── css/
        └── site.css               # CSS tùy chỉnh
```

## 🚀 Hướng Dẫn Cài Đặt

### Yêu Cầu Hệ Thống
- .NET 9.0 SDK
- SQL Server 2019 hoặc SQL Server LocalDB
- Visual Studio 2022 hoặc VS Code
- Git

### Các Bước Cài Đặt

1. **Clone repository**
```bash
git clone <repository-url>
cd Shopbanhangapple
```

2. **Restore packages**
```bash
dotnet restore
```

3. **Tạo database**
```bash
dotnet ef database update
```

4. **Chạy ứng dụng**
```bash
dotnet run
```

5. **Truy cập ứng dụng**
- Mở trình duyệt và truy cập: `http://localhost:5159`

### Cấu Hình Database

Dự án sử dụng **SQL Server LocalDB** mặc định. Connection string trong `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AppleStoreDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

**Nếu muốn sử dụng SQL Server khác:**

1. Cập nhật connection string trong `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=AppleStoreDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True"
  }
}
```

2. Chạy lại migrations:
```bash
dotnet ef database update
```


## 📊 Database Schema

### Tables
- **AspNetUsers**: Quản lý người dùng (Identity)
- **Categories**: Danh mục sản phẩm
- **Products**: Sản phẩm
- **ProductConfigurations**: Cấu hình tùy chỉnh sản phẩm
- **Orders**: Đơn hàng
- **OrderDetails**: Chi tiết đơn hàng

### Relationships
- Category 1-N Products
- Product 1-N ProductConfigurations
- Order 1-N OrderDetails
- Product 1-N OrderDetails

## 🎨 Giao Diện

Website được thiết kế theo phong cách Apple với:
- **Màu sắc**: Tông màu tối, xám và xanh Apple
- **Typography**: San Francisco font (Apple system font)
- **Hiệu ứng**: Hover effects, smooth transitions
- **Responsive**: Tương thích với mọi thiết bị
- **Modern UI**: Card-based layout, glassmorphism effects

## 📝 Dữ Liệu Mẫu

Hệ thống tự động tạo dữ liệu mẫu bao gồm:
- 5 danh mục sản phẩm
- 8 sản phẩm mẫu (iPhone, Mac, iPad, Watch, Phụ kiện)
- Cấu hình tùy chỉnh cho các sản phẩm có thể tùy biến

## 🔐 Bảo Mật

- **Authentication**: ASP.NET Core Identity
- **Authorization**: Role-based access control
- **Session**: Secure session management
- **HTTPS**: Hỗ trợ HTTPS
- **CSRF Protection**: Anti-forgery tokens

## 🎯 Use Cases

### Khách Hàng
1. Duyệt và tìm kiếm sản phẩm
2. Xem chi tiết sản phẩm và thông số kỹ thuật
3. Tùy chỉnh cấu hình sản phẩm (RAM, SSD, màu sắc)
4. Thêm sản phẩm vào giỏ hàng
5. Đặt hàng và thanh toán
6. Theo dõi trạng thái đơn hàng
7. Xem lịch sử mua hàng

### Quản Trị Viên
1. Quản lý sản phẩm (thêm, sửa, xóa)
2. Cập nhật tồn kho và giá cả
3. Quản lý đơn hàng
4. Cập nhật trạng thái đơn hàng
5. Xem thống kê doanh thu
6. Quản lý cấu hình sản phẩm tùy chỉnh

## 📞 Liên Hệ

- **Email**: support@applestore.vn
- **Phone**: 1900-xxxx
- **Address**: Hà Nội, Việt Nam

## 📄 License

Copyright © 2025 Apple Store Vietnam. All rights reserved.

---

**Developed with ❤️ using ASP.NET Core MVC**
