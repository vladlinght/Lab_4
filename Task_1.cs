using System;
using System.Collections.Generic;
using System.Linq;

// Клас "Товар"
public class Product : ISearchable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public int Rating { get; set; }

    public Product(string name, decimal price, string description, string category, int rating)
    {
        Name = name;
        Price = price;
        Description = description;
        Category = category;
        Rating = rating;
    }

    public override string ToString()
    {
        return $"{Name} ({Category}) - ${Price} - Rating: {Rating}";
    }
}

// Клас "Користувач"
public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Order> PurchaseHistory { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        PurchaseHistory = new List<Order>();
    }
}


public class Order
{
    public List<Product> Products { get; set; }
    public int Quantity { get; set; }
    public decimal TotalCost { get; set; }
    public string Status { get; set; }

    public Order(List<Product> products, int quantity, decimal totalCost, string status)
    {
        Products = products;
        Quantity = quantity;
        TotalCost = totalCost;
        Status = status;
    }
}
public interface ISearchable
{
    bool MatchesSearchCriteria(string criteria);
}


public class Store
{
    public List<Product> Products { get; set; }
    public List<User> Users { get; set; }
    public List<Order> Orders { get; set; }

    public Store()
    {
        Products = new List<Product>();
        Users = new List<User>();
        Orders = new List<Order>();
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void CreateOrder(User user, List<Product> products, int quantity)
    {
        decimal totalCost = products.Sum(p => p.Price) * quantity;
        Order order = new Order(products, quantity, totalCost, "Pending");
        user.PurchaseHistory.Add(order);
        Orders.Add(order);
    }

    public List<Product> SearchProducts(string criteria)
    {
        return Products.Where(p => p.MatchesSearchCriteria(criteria)).ToList();
    }

    public void DisplayProductList(List<Product> productList)
    {
        foreach (var product in productList)
        {
            Console.WriteLine(product);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Store store = new Store();

        

        Product product1 = new Product("Laptop", 800, "High-performance laptop", "Electronics", 5);
        Product product2 = new Product("Smartphone", 500, "Latest smartphone model", "Electronics", 4);
        Product product3 = new Product("Book", 20, "Best-selling novel", "Books", 4);

        store.AddProduct(product1);
        store.AddProduct(product2);
        store.AddProduct(product3);

        User user1 = new User("user1", "password1");
        User user2 = new User("user2", "password2");

        store.AddUser(user1);
        store.AddUser(user2);

        
        Console.WriteLine("Search results for 'Electronics':");
        var electronicProducts = store.SearchProducts("Electronics");
        store.DisplayProductList(electronicProducts);

        Console.WriteLine("Search results for 'Book':");
        var bookProducts = store.SearchProducts("Book");
        store.DisplayProductList(bookProducts);

        
        store.CreateOrder(user1, electronicProducts, 2);
        store.CreateOrder(user2, bookProducts, 3);

        Console.WriteLine("User 1's Purchase History:");
        foreach (var order in user1.PurchaseHistory)
        {
            Console.WriteLine($"Order status: {order.Status}, Total Cost: ${order.TotalCost}");
        }
    }
}
