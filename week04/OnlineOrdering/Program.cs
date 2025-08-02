using System;

class Program
{
    static void Main(string[] args)
    {
        // Order 1 - USA
        Address address1 = new Address("123 Apple St", "New York", "NY", "USA");
        Customer customer1 = new Customer("John Doe", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("T-shirt", "TS001", 15.0, 2));
        order1.AddProduct(new Product("Hat", "HT002", 10.0, 1));

        // Order 2 - International
        Address address2 = new Address("456 Maple Ave", "Toronto", "ON", "Canada");
        Customer customer2 = new Customer("Jane Smith", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Shoes", "SH003", 50.0, 1));
        order2.AddProduct(new Product("Socks", "SK004", 5.0, 3));

        // Display Orders
        DisplayOrder(order1);
        Console.WriteLine("----------------------------");
        DisplayOrder(order2);
    }

    static void DisplayOrder(Order order)
    {
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order.GetTotalPrice():0.00}");
    }
}
