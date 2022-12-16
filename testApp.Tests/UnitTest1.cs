using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using testApp.Data;
using testApp.Models;
using testApp.Services;

namespace testApp.Tests
{
    public class UnitTest1
    {
        static DbContextOptions<ApplicationDbContext> option = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Products").Options;
        ProductService productService = new ProductService(new ApplicationDbContext(option));
        public void Init()
        {
            if (productService.Get().Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    productService.Create(new Product()
                    {
                        Name = i.ToString(),
                        Price = i,
                        Stock = i,
                        Description = i.ToString(),
                    });
                }
            }
            else return;
            
        }
        public UnitTest1()
        {
            Init();
        }
        [Fact]
        public void Get_All_Return_TenProducts()
        {
            var result = productService.Get();
            var actual = result.Count();
            Assert.Equal(10, actual);
        }
        [Fact]
        public void Find_ShouldntBeNull()
        {
            var result = productService.GetById(2);
            Assert.NotNull(result);
        }
        [Fact]
        public void Delete_One_ShouldReturn_NineProducts()
        {
            productService.Delete(1);
            var result = productService.Get().Count();
            Assert.Equal(9, result);
        }
    }
}