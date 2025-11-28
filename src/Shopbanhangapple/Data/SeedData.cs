using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Shopbanhangapple.Data;
using Shopbanhangapple.Models;

namespace Shopbanhangapple
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create Roles
            string[] roleNames = { "Admin", "Customer" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Users first
            if (!context.Users.Any())
            {
                // Create admin user
                var adminUser = new IdentityUser
                {
                    UserName = "admin@applestore.vn",
                    Email = "admin@applestore.vn",
                    EmailConfirmed = true
                };
                var adminResult = await userManager.CreateAsync(adminUser, "Admin@123");
                if (adminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                // Create test users
                var user1 = new IdentityUser
                {
                    UserName = "user1@example.com",
                    Email = "user1@example.com",
                    EmailConfirmed = true
                };
                var user1Result = await userManager.CreateAsync(user1, "User@123");
                if (user1Result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user1, "Customer");
                }

                var user2 = new IdentityUser
                {
                    UserName = "user2@example.com",
                    Email = "user2@example.com",
                    EmailConfirmed = true
                };
                var user2Result = await userManager.CreateAsync(user2, "User@123");
                if (user2Result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user2, "Customer");
                }
            }

            // Check if database has data
            if (context.Categories.Any())
            {
                return; // DB has been seeded
            }

            // Seed Categories
            var categories = new Category[]
            {
                new Category { Name = "iPhone", Description = "Điện thoại thông minh iPhone" },
                new Category { Name = "Mac", Description = "Máy tính Mac" },
                new Category { Name = "iPad", Description = "Máy tính bảng iPad" },
                new Category { Name = "Watch", Description = "Đồng hồ thông minh Apple Watch" },
                new Category { Name = "Phụ kiện", Description = "Phụ kiện Apple chính hãng" }
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            // Seed Products
            var products = new Product[]
            {
                new Product
                {
                    Name = "iPhone 15 Pro Max",
                    Description = "iPhone 15 Pro Max với chip A17 Pro mạnh mẽ, camera 48MP",
                    Price = 29990000,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/42/305658/iphone-15-pro-max-blue-thumbnew-600x600.jpg",
                    TechnicalSpecs = "Màn hình: 6.7 inch, Chip: A17 Pro, Camera: 48MP, Pin: 4422mAh",
                    Stock = 50,
                    IsCustomizable = true,
                    CategoryId = 1
                },
                new Product
                {
                    Name = "MacBook Pro 14 M3",
                    Description = "MacBook Pro 14 inch với chip M3 mạnh mẽ",
                    Price = 45990000,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/44/309016/apple-macbook-pro-14-m3-2023-mrx33saa-thumb-600x600.jpg",
                    TechnicalSpecs = "Màn hình: 14.2 inch, Chip: M3, RAM: 8GB, SSD: 512GB",
                    Stock = 30,
                    IsCustomizable = true,
                    CategoryId = 2
                },
                new Product
                {
                    Name = "iPad Pro 12.9 M2",
                    Description = "iPad Pro 12.9 inch với chip M2 siêu mạnh",
                    Price = 32990000,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/522/289700/ipad-pro-12-9-inch-wifi-128gb-2022-thumb-600x600.jpg",
                    TechnicalSpecs = "Màn hình: 12.9 inch, Chip: M2, RAM: 8GB, Bộ nhớ: 128GB",
                    Stock = 25,
                    IsCustomizable = false,
                    CategoryId = 3
                },
                new Product
                {
                    Name = "Apple Watch Series 9",
                    Description = "Apple Watch Series 9 với màn hình luôn bật",
                    Price = 10990000,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/7077/309076/apple-watch-s9-lte-41mm-vien-nhom-day-cao-su-thumb-600x600.jpg",
                    TechnicalSpecs = "Màn hình: 1.9 inch, Chip: S9, Chống nước: 50m",
                    Stock = 40,
                    IsCustomizable = false,
                    CategoryId = 4
                },
                new Product
                {
                    Name = "AirPods Pro 2",
                    Description = "AirPods Pro thế hệ 2 với chống ồn chủ động",
                    Price = 6490000,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/54/289780/tai-nghe-bluetooth-airpods-pro-2-apple-thumb-600x600.jpg",
                    TechnicalSpecs = "Chống ồn chủ động, Chip H2, Thời lượng pin: 6 giờ",
                    Stock = 100,
                    IsCustomizable = false,
                    CategoryId = 5
                },
                new Product
                {
                    Name = "iPhone 14 Pro",
                    Description = "iPhone 14 Pro với Dynamic Island độc đáo",
                    Price = 24990000,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/42/289700/iphone-14-pro-tim-thumb-600x600.jpg",
                    TechnicalSpecs = "Màn hình: 6.1 inch, Chip: A16 Bionic, Camera: 48MP",
                    Stock = 35,
                    IsCustomizable = true,
                    CategoryId = 1
                },
                new Product
                {
                    Name = "MacBook Air M2",
                    Description = "MacBook Air M2 siêu mỏng nhẹ",
                    Price = 28990000,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/44/282827/apple-macbook-air-m2-2022-thumb-600x600.jpg",
                    TechnicalSpecs = "Màn hình: 13.6 inch, Chip: M2, RAM: 8GB, SSD: 256GB",
                    Stock = 45,
                    IsCustomizable = true,
                    CategoryId = 2
                },
                new Product
                {
                    Name = "iPad Air 5",
                    Description = "iPad Air thế hệ 5 với chip M1",
                    Price = 16990000,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/522/247517/ipad-air-5-wifi-purple-thumb-600x600.jpg",
                    TechnicalSpecs = "Màn hình: 10.9 inch, Chip: M1, RAM: 8GB",
                    Stock = 30,
                    IsCustomizable = false,
                    CategoryId = 3
                }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();

            // Seed Product Configurations for customizable products
            var configurations = new ProductConfiguration[]
            {
                // iPhone 15 Pro Max configurations
                new ProductConfiguration { ProductId = 1, OptionType = "Dung lượng", OptionValue = "256GB", PriceAdjustment = 0 },
                new ProductConfiguration { ProductId = 1, OptionType = "Dung lượng", OptionValue = "512GB", PriceAdjustment = 5000000 },
                new ProductConfiguration { ProductId = 1, OptionType = "Dung lượng", OptionValue = "1TB", PriceAdjustment = 10000000 },
                new ProductConfiguration { ProductId = 1, OptionType = "Màu sắc", OptionValue = "Titan Xanh", PriceAdjustment = 0 },
                new ProductConfiguration { ProductId = 1, OptionType = "Màu sắc", OptionValue = "Titan Đen", PriceAdjustment = 0 },
                new ProductConfiguration { ProductId = 1, OptionType = "Màu sắc", OptionValue = "Titan Trắng", PriceAdjustment = 0 },

                // MacBook Pro 14 M3 configurations
                new ProductConfiguration { ProductId = 2, OptionType = "RAM", OptionValue = "8GB", PriceAdjustment = 0 },
                new ProductConfiguration { ProductId = 2, OptionType = "RAM", OptionValue = "16GB", PriceAdjustment = 5000000 },
                new ProductConfiguration { ProductId = 2, OptionType = "RAM", OptionValue = "32GB", PriceAdjustment = 12000000 },
                new ProductConfiguration { ProductId = 2, OptionType = "SSD", OptionValue = "512GB", PriceAdjustment = 0 },
                new ProductConfiguration { ProductId = 2, OptionType = "SSD", OptionValue = "1TB", PriceAdjustment = 6000000 },
                new ProductConfiguration { ProductId = 2, OptionType = "SSD", OptionValue = "2TB", PriceAdjustment = 14000000 },

                // iPhone 14 Pro configurations
                new ProductConfiguration { ProductId = 6, OptionType = "Dung lượng", OptionValue = "128GB", PriceAdjustment = 0 },
                new ProductConfiguration { ProductId = 6, OptionType = "Dung lượng", OptionValue = "256GB", PriceAdjustment = 3000000 },
                new ProductConfiguration { ProductId = 6, OptionType = "Dung lượng", OptionValue = "512GB", PriceAdjustment = 7000000 },

                // MacBook Air M2 configurations
                new ProductConfiguration { ProductId = 7, OptionType = "RAM", OptionValue = "8GB", PriceAdjustment = 0 },
                new ProductConfiguration { ProductId = 7, OptionType = "RAM", OptionValue = "16GB", PriceAdjustment = 5000000 },
                new ProductConfiguration { ProductId = 7, OptionType = "SSD", OptionValue = "256GB", PriceAdjustment = 0 },
                new ProductConfiguration { ProductId = 7, OptionType = "SSD", OptionValue = "512GB", PriceAdjustment = 5000000 }
            };

            context.ProductConfigurations.AddRange(configurations);
            await context.SaveChangesAsync();
        }
    }
}
