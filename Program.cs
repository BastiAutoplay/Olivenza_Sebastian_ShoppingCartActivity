using System;
using System.ComponentModel.Design;

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

        Product[] cart = new Product[10];
        int[] cartQuantity = new int[10];
        int cartCount = 0;

        char choice;

        while (true)
        {
            Console.WriteLine("\n=== STORE MENU ===");
            foreach (Product p in products)
            {
                p.DisplayProduct();
            }

            int productChoice;
            Console.Write("\nEnter product number: ");

            if (!int.TryParse(Console.ReadLine(), out productChoice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            if (productChoice < 1 || productChoice > products.Length)
            {
                Console.WriteLine("Invalid product number.");
                continue;
            }

            int quantity;
            Console.Write("Enter quantity: ");

            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                continue;
            }

            Product selectedProduct = products[productChoice - 1];

            if (selectedProduct.RemainingStock == 0)
            {
                Console.WriteLine("Out of stock.");
                continue;
            }

            if (!selectedProduct.HasEnoughStock(quantity))
            {
                Console.WriteLine("Not enough stock.");
                continue;
            }

            // Check duplicate in cart
            bool found = false;
            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].Id == selectedProduct.Id)
                {
                    cartQuantity[i] += quantity;
                    found = true;
                    break;
                }
            }

            // If not duplicate, add new item
            if (!found)
            {
                if (cartCount >= cart.Length)
                {
                    Console.WriteLine("Cart is full.");
                    continue;
                }

                cart[cartCount] = selectedProduct;
                cartQuantity[cartCount] = quantity;
                cartCount++;
            }

            selectedProduct.DeductStock(quantity);

            Console.WriteLine($"Added to cart: {selectedProduct.Name} x{quantity}");

            Console.Write("\nAdd more items? (Y/N): ");
            choice = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            if (choice == 'Y')
            {
                break; // continue shopping
            }
            else if (choice == 'N')
            {
                goto Checkout; // exit loop properly
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter Y or N only.");
            }
        }

        Checkout:

        // Display cart
        Console.WriteLine("\n=== RECEIPT ===");

        double grandTotal = 0;

        for (int i = 0; i < cartCount; i++)
        {
            double subtotal = cart[i].GetItemTotal(cartQuantity[i]);
            grandTotal += subtotal;

            Console.WriteLine($"{cart[i].Name} x{cartQuantity[i]} = ₱{subtotal}");
        }

        Console.WriteLine($"Grand Total: ₱{grandTotal}");

        //Discount
        double discount = 0;

        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
            Console.WriteLine($"Discount (10%): ₱{discount}");
        }

        double finalTotal = grandTotal - discount;

        Console.WriteLine($"Final Total: ₱{finalTotal}");

        //Show Updated Stock
        Console.WriteLine("\n=== UPDATED STOCK ===");

        foreach (Product p in products)
        {
            Console.WriteLine($"{p.Name} - Remaining: {p.RemainingStock}");
        }
    }
}
