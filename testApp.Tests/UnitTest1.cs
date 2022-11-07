using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using testApp.Models;
using testApp.Services;

namespace testApp.Tests
{
    public class UnitTest1
    {
        static DbContextOptions<MarketContext> option = new DbContextOptionsBuilder<MarketContext>()
                .UseInMemoryDatabase(databaseName: "Products").Options;
        ProductService productService = new ProductService(new MarketContext(option));
        public async Task Init()
        {
            if (productService.Get().Result.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    await productService.Create(new Product()
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
        public async void Get_All_Return_TenProducts()
        {
            var result = await productService.Get();
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
            var result = productService.Get().Result.Count();
            Assert.Equal(9, result);
        }
        [Fact]
        public void Fail()
        {
            Assert.True(false);
        }
    }
}