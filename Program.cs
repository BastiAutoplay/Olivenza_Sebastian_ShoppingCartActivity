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

    int productChoice;
    Console.Write("\nEnter product number: ");

    if (!int.TryParse(Console.ReadLine(), out productChoice))
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        return;
    }

    if (productChoice < 1 || productChoice > products.Length)
    {
        Console.WriteLine("Invalid product number.");
        return;
    }

    int quantity;
    Console.Write("Enter quantity: ");

    if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
    {
        Console.WriteLine("Invalid quantity.");
        return;
    }

    Product selectedProduct = products[productChoice - 1];

    if (selectedProduct.RemainingStock == 0)
    {
        Console.WriteLine("This product is out of stock.");
        return;
    }

    if (!selectedProduct.HasEnoughStock(quantity))
    {
        Console.WriteLine("Not enough stock available.");
        return;
    }

    double total = selectedProduct.GetItemTotal(quantity);

    Console.WriteLine($"\nAdded to cart: {selectedProduct.Name} x{quantity}");
    Console.WriteLine($"Subtotal: ₱{total}");

    selectedProduct.DeductStock(quantity);

    Console.WriteLine($"Remaining stock: {selectedProduct.RemainingStock}");
}
