using System;
using System.Collections.Generic;

namespace testApp.Models
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
            Categories = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int Stock { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
