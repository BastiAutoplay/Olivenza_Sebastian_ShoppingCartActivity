using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;

    public void DisplayProduct()
    {
        Console.WriteLine($"{Id}. {Name} - ₱{Price} (Stock: {RemainingStock})");
    }

    public bool HasEnoughStock(int quantity)
    {
        return quantity <= RemainingStock;
    }

    public double GetItemTotal(int quantity)
    {
        return Price * quantity;
    }

    public void DeductStock(int quantity)
    {
        RemainingStock -= quantity;
    }
}

class Program
{
    static void Main()
{
    Product[] products = new Product[]
    {
        new Product { Id = 1, Name = "Laptop", Price = 30000, RemainingStock = 5 },
        new Product { Id = 2, Name = "Phone", Price = 15000, RemainingStock = 10 },
        new Product { Id = 3, Name = "Headphones", Price = 2000, RemainingStock = 15 }
    };

    Console.WriteLine("=== STORE MENU ===");
    foreach (Product p in products)
    {
        p.DisplayProduct();
    }
}
