using System;
using System.Linq;

class Program
{
    static void Main()
    {
        using (var context = new StoreContext())
        {
            var newProduct = new Product
            {
                ProductName = "New Product",
                Description = "Description of the new product",
                Price = 10.99m,
                QuantityInStock = 100
            };
            context.Products.Add(newProduct);
            context.SaveChanges();

            var productToUpdate = context.Products.Single(p => p.ProductName == "New Product");
            productToUpdate.Price = 12.99m;
            context.SaveChanges();

            var userOrders = context.Orders.Where(o => o.User.UserName == "Username").ToList();

            var orderTotalCosts = context.OrderDetails
                .GroupBy(od => od.OrderID)
                .Select(g => new { OrderID = g.Key, TotalCost = g.Sum(od => od.TotalCost) })
                .ToList();

            var totalStock = context.Products.Sum(p => p.QuantityInStock);


            var expensiveProducts = context.Products.OrderByDescending(p => p.Price).Take(5).ToList();

            var lowStockProducts = context.Products.Where(p => p.QuantityInStock < 5).ToList();
        }
    }
}
