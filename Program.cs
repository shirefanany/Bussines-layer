//for (int i = 1; i <= 50; i++)
//{
//    string output = "";

//    if (i % 3 == 0)
//    {
//        output += "Nursing";
//    }
//    if (i % 7 == 0)
//    {
//        output += "Meliora";
//    }

//    Console.WriteLine(output == "" ? i.ToString() : output);
//}



// this model for every customer
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

class Program
{
    static void Main()
    {
        var kristenBusiness = new KristenCookieBusiness();

        // for place order
        kristenBusiness.PlaceOrder();

        // Retrieve order history
        var orderHistory = kristenBusiness.GetOrderHistory();
        PrintOrderHistory(orderHistory);

        // Check if Kristen should scale up
        if (kristenBusiness.ShouldScaleUp())
        {
            Console.WriteLine("Kristen should consider scaling up her baking space.");
        }
    }
    static void PrintOrderHistory(List<CookieOrder> orders)
    {
        foreach (var order in orders)
        {
            Console.WriteLine($" Order ID: {order.OrderId}," +
                              $" Customer: {order.CustomerName}," +
                              $" Number Of Cookies: {order.NumOfCookies}," +
                              $" Toppings: {string.Join(", ", order.Toppings)},");
        }
    }


}

// Define a class to represent a Cookie Order
class CookieOrder
{
    public int OrderId { get; set; }
    public int NumOfCookies { get; set; }
    public required string CustomerName { get; set; } // required based on her bussines palaning (who orderd)
    public required string CustomerEmail { get; set; }// required based on her bussines palaning (order placed by email)
    public  List<string>? Toppings { get; set; }
    public DateTime OrderDate { get; set; }
}

// Define a class to manage Kristen's business operations
class KristenCookieBusiness
{
    private List<CookieOrder> ordersList;

    public KristenCookieBusiness()
    {
        ordersList = new List<CookieOrder>();
    }
   
    // Method to place a new order
    public void PlaceOrder()
    {
        while (true)
        {
            int i = 0;
            Console.WriteLine("Enter the Name of Customer (or leave empty to finish adding new order):");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
                break;



            int numOfCookies;
            while (true)
            {
                Console.WriteLine("Enter Number of Cookies (min 12 cookies)");
                numOfCookies = Convert.ToInt32(Console.ReadLine());
                if (numOfCookies >= 12)
                    break;
            }

            string email;
            while (true)
            {
                Console.WriteLine("You Must Enter a Valid Email Address");
                email = Console.ReadLine();
                if (email.Contains("@") && !string.IsNullOrEmpty(email) && !string.IsNullOrWhiteSpace(email))
                {
                    break;
                }
            }

            List<string> toppings = new List<string>();

            while (true)
            {
                Console.WriteLine("Enter a topping for all cookies (or leave empty to finish toppings):");

                string topping = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(topping))
                    break;

                toppings.Add(topping);
            }

            var order = new CookieOrder
            {
                OrderId = ordersList.Count + 1,
                NumOfCookies = numOfCookies,
                CustomerName = name,
                CustomerEmail = email,
                Toppings = toppings,
                OrderDate = DateTime.Now
            };

            ordersList.Add(order);
        }
        
    }


    // Method to retrieve order history based bussines plaining
    public List<CookieOrder> GetOrderHistory()
    {
        return ordersList;
    }

    // Method to plan for business scaling
    public bool ShouldScaleUp()
    {

        // We can Implement logic to determine when Kristen should scale up based on order volume and trends.
        // We can consider factors like order frequency and quantity per one month from now.


        // Calculate the date one month ago from the current date
        DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);

        // Count the number of orders placed within the last month
        int ordersInLastMonth = ordersList.Count(order => order.OrderDate >= oneMonthAgo);

        return ordersInLastMonth >= 50; // Scale up if more than 50 orders in the last month.
    }

}

