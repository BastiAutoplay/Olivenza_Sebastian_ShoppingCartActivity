# Shopping Cart System (C#)

This is a simple console-based shopping cart system made in C#.  
It uses classes, arrays, and basic input validation.

---

## 📌 Overview

This pull request contains the implementation of Part 2 of the Shopping Cart System.

It expands the original program by adding cart management features, payment processing, receipt improvements, and order tracking.

---

## Features

- Display product menu
- User selects product and quantity
- Input validation using `int.TryParse()`
- Stock checking (prevents overbuying)
- Add items to cart
- Prevent duplicate items (updates quantity instead)
- Computes total and applies 10% discount if total ≥ ₱5000
- Displays receipt and updated stock

---

## 🚀 Features Added

### 🛒 Cart Management
- View cart items
- Update item quantity
- Remove items from cart
- Clear entire cart
- Checkout option

### 💳 Payment System
- Accepts user payment input
- Validates numeric input using `TryParse`
- Prevents insufficient payment
- Calculates and displays change

### 🧾 Receipt Enhancements
- Displays receipt number
- Shows current date and time
- Displays total, discount, and final amount

### 📦 Inventory Improvements
- Updates product stock after purchase
- Displays updated stock after checkout
- Low stock alert for items with stock ≤ 5

### 📜 Order History
- Stores completed transactions
- Displays previous receipts with:
  - Receipt number
  - Total amount
  - Date and time

---

## 🧠 Improvements Made

- Strengthened input validation (menu options and Y/N prompts)
- Organized logic for better readability
- Improved user interaction flow

---

## 🧪 Notes

- All features were implemented step-by-step with multiple commits
- Program was tested with invalid inputs to ensure stability
- Order history is stored in memory (resets when program restarts)

## How to Run

1. Open in Visual Studio or any C# IDE
2. Run the program
3. Follow the instructions in the console

---

## Flowchart

A flowchart of the program is included in this repository.

---

## 📷 Sample Output

<img width="1919" height="991" alt="image" src="https://github.com/user-attachments/assets/56e2c537-186d-416d-8413-bf20c6b04f11" />

---

## AI Usage in This Project

AI was used as a guide to help understand and implement parts of the program.

### Which parts used AI:
- Input validation using `int.TryParse()`
- Cart system logic (handling duplicates and quantities)
- Loop structure for continuous user input
- Discount and receipt computation

### Why AI was used:
- To better understand how to structure the program using classes and arrays
- To learn proper validation techniques in C#
- To help debug and improve logic flow

### Prompts/questions asked:
- "How to validate user input in C# using TryParse?"
- "How to create a shopping cart system using arrays?"
- "How to prevent duplicate items in a cart?"

### What I changed after using AI:
- Adjusted variable names to make them clearer
- Simplified some conditions for better readability
- Modified the flow to match the project requirements
- Ensured I understood each part before finalizing the code

---

## Author

Sebastian V. Olivenza
