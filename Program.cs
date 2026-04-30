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

    static void ShowCartMenu()
    {
        Console.WriteLine("\n=== CART MENU ===");
        Console.WriteLine("1. View Cart");
        Console.WriteLine("2. Update Item Quantity");
        Console.WriteLine("3. Remove Item");
        Console.WriteLine("4. Clear Cart");
        Console.WriteLine("5. Checkout");
        Console.WriteLine("6. View Order History");
    }
    static void Main()
    {
        Product[] products = new Product[]
        {
        new Product { Id = 1, Name = "Laptop", Price = 30000, RemainingStock = 10 },
        new Product { Id = 2, Name = "Phone", Price = 15000, RemainingStock = 20 },
        new Product { Id = 3, Name = "Headphones", Price = 2000, RemainingStock = 30 }
        };

        Product[] cart = new Product[10];
        string[] orderHistory = new string[20];
        int orderCount = 0;
        int[] cartQuantity = new int[10];
        int cartCount = 0;
        int receiptCounter = 1;
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

            bool managingCart = true;

            while (managingCart)
            {
                ShowCartMenu();
                Console.Write("Choose option: ");

                int option;
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        Console.WriteLine("\n=== YOUR CART ===");
                        for (int i = 0; i < cartCount; i++)
                        {
                            Console.WriteLine($"{i + 1}. {cart[i].Name} x{cartQuantity[i]}");
                        }
                        break;

                    case 2:
                        Console.Write("Enter item number to update: ");
                        int updateIndex;
                        if (!int.TryParse(Console.ReadLine(), out updateIndex) || updateIndex < 1 || updateIndex > cartCount)
                        {
                            Console.WriteLine("Invalid selection.");
                            break;
                        }

                        Console.Write("Enter new quantity: ");
                        int newQty;
                        if (!int.TryParse(Console.ReadLine(), out newQty) || newQty <= 0)
                        {
                            Console.WriteLine("Invalid quantity.");
                            break;
                        }

                        cartQuantity[updateIndex - 1] = newQty;
                        Console.WriteLine("Quantity updated.");
                        break;

                    case 3:
                        Console.Write("Enter item number to remove: ");
                        int removeIndex;
                        if (!int.TryParse(Console.ReadLine(), out removeIndex) || removeIndex < 1 || removeIndex > cartCount)
                        {
                            Console.WriteLine("Invalid selection.");
                            break;
                        }

                        for (int i = removeIndex - 1; i < cartCount - 1; i++)
                        {
                            cart[i] = cart[i + 1];
                            cartQuantity[i] = cartQuantity[i + 1];
                        }

                        cartCount--;
                        Console.WriteLine("Item removed.");
                        break;

                    case 4:
                        cartCount = 0;
                        Console.WriteLine("Cart cleared.");
                        break;

                    case 5:
                        managingCart = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;

                    case 6:
                        Console.WriteLine("\n=== ORDER HISTORY ===");

                        if (orderCount == 0)
                        {
                            Console.WriteLine("No orders yet.");
                            break;
                        }

                        for (int i = 0; i < orderCount; i++)
                        {
                            Console.WriteLine(orderHistory[i]);
                        }
                        break;
                }
            }

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


        Console.WriteLine($"Receipt No: {receiptCounter:D4}");
        Console.WriteLine($"Date: {DateTime.Now:MMMM dd, yyyy hh:mm tt}");

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

        double payment;

        while (true)
        {
            Console.Write("\nEnter payment: ");

            if (!double.TryParse(Console.ReadLine(), out payment))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            if (payment < finalTotal)
            {
                Console.WriteLine("Insufficient payment.");
                continue;
            }

            break;
        }

        double change = payment - finalTotal;

        Console.WriteLine($"Payment: ₱{payment}");
        Console.WriteLine($"Change: ₱{change}");
        receiptCounter++;

        // Save to order history
        string receiptRecord = $"Receipt No: {receiptCounter - 1:D4} | Total: ₱{finalTotal} | Date: {DateTime.Now:MMMM dd, yyyy hh:mm tt}";

        orderHistory[orderCount] = receiptRecord;
        orderCount++;

        //Show Updated Stock
        Console.WriteLine("\n=== UPDATED STOCK ===");

        foreach (Product p in products)
        {
            Console.WriteLine($"{p.Name} - Remaining: {p.RemainingStock}");
        }

        Console.WriteLine("\n=== LOW STOCK ALERT ===");

        bool hasLowStock = false;

        foreach (Product p in products)
        {
            if (p.RemainingStock <= 5)
            {
                Console.WriteLine($"{p.Name} has only {p.RemainingStock} stocks left.");
                hasLowStock = true;
            }
        }

        if (!hasLowStock)
        {
            Console.WriteLine("All products have sufficient stock.");
        }
    }
}
