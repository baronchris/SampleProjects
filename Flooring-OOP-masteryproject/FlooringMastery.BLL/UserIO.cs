using FlooringMastery.BLL;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorData;

namespace FlooringMastery
{
    public static class UserIO
    {
        public static int GetIntegerFromUser()
        {
            bool goodInt = false;
            int result=0;
            while (!goodInt)
            {
               
                string input = Console.ReadLine();
                goodInt = int.TryParse(input, out result);
                if (goodInt == true) { break; }
                else { Console.WriteLine("Please enter an integer"); }
            }
            return result;
        }
        public static void PrintOrders(string filename)
        {
            LoadOrders O = new LoadOrders();
            string path = @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\Orders\" + filename;
            if (File.Exists(path))
            {
                List<Order> Orders = O.ParseFileData(filename);
                foreach (Order order in Orders)
                {
                    Console.WriteLine($"Order Number:  {order.OrderNumber}");
                    Console.WriteLine($"Customer Name: {order.CustomerName}");
                    Console.WriteLine($"State:  {order.State}");
                    Console.WriteLine($"Product:  {order.Product}");
                    Console.WriteLine($"Cost of Materials:  {order.MaterialCost}");
                    Console.WriteLine($"Cost of Labor:  {order.LaborCost:C}");
                    Console.WriteLine($"Tax:  {order.Tax:C}");
                    Console.WriteLine($"Total:  {order.Total:C}");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                }
                Console.WriteLine("Press any key to continue");
            }
            else
            {
                Console.WriteLine("No orders found for that date");
                Console.WriteLine("Press any key to continue");
            }
        }
        public static void DisplayOrder(string filename, int ordernumber)
        {
            LoadOrders O = new LoadOrders();
            List<Order> Orders = O.ParseFileData(filename);
            if(Orders == null)
            {
                Console.WriteLine("no orders found");
                return;
            }
            Order selected = Orders.Where(o => o.OrderNumber == ordernumber).FirstOrDefault();
            if(selected != null)
            {
                Console.WriteLine($"Order Number:  {selected.OrderNumber}");
                Console.WriteLine($"Customer Name: {selected.CustomerName}");
                Console.WriteLine($"State:  {selected.State}");
                Console.WriteLine($"Product:  {selected.Product}");
                Console.WriteLine($"Cost of Materials:  {selected.MaterialCost:C}");
                Console.WriteLine($"Cost of Labor:  {selected.LaborCost:C}");
                Console.WriteLine($"Tax:  {selected.Tax:C}");
                Console.WriteLine($"Total:  {selected.Total:C}");
                
            }
            else
            {
                Console.WriteLine("No order with that number was found for this date");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Press any key to continue");
        }

        public static void DisplayProducts()
        {
            List<Product> Products = new List<Product>();
            try
            {
                Products = LoadProducts.LoadAllProducts();
            }
            catch
            {
                Console.WriteLine("Could not load product data");
            }
            if (Products != null)
            {
                Console.WriteLine("Product List");
                int i = 1;
                foreach (Product product in Products)
                {
                    Console.WriteLine($"{i}: Product Name: {product.ProductName}");
                    Console.WriteLine($"Price per square foot: {product.CostperSquare:C}");
                    Console.WriteLine($"Labor cost per square foot: {product.LaborCost:C}");
                    Console.WriteLine("_________________________________________________");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Error displaying product information.\nTry turning the computer off and on before contacting Contact IT");
            }
           
        }
        public static void DisplayStates()
        {
            List<State> States = new List<State>();
            try
            {
                States = LoadStates.LoadAllStates();
            }
            catch
            {
                Console.WriteLine("Could not load sales tax data");
            }
            if (States != null)
            {
                Console.WriteLine("List of Sales Zones");
                int i = 1;
                foreach (State state in States)
                {
                    Console.WriteLine($"{i}: State Name: {state.StateName}");
                    Console.WriteLine($"State Code: {state.StateCode}");
                    Console.WriteLine($"Tax Rate: {state.TaxRate}%");
                    Console.WriteLine("_________________________________________________");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Error displaying tax information.\nTry turning the computer off and on before contacting Contact IT");
            }
            
        }
        public static void DisplayCurrentOrderFiles()
        {
            Console.Clear();
            List<string> allfiles = FileIO.GetCurrentOrderFiles();
            LoadOrders o = new LoadOrders();
            foreach (string file in allfiles)
            {

                int x = file.Length;
                string sub = file.Substring(77, 8);
                string output = sub.Substring(0, 2) + "/" + sub.Substring(2, 2) + "/" + sub.Substring(4, 4); 
                int OrderNumber = o.GetHigestOrderNumberinFile(file);
                Console.WriteLine($"File Date: {output}  Final order number:  {OrderNumber}");
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
            }
            Console.WriteLine($"The highest order number is {o.GetHighestOrderNumber(allfiles)}");
        }
        
        public static void Displaydictionary()
        {
            Console.WriteLine("Dictionary Load Test");

            LoadOrders o = new LoadOrders();
            Dictionary<int, Order> OrderDirectory = o.ParseOrderFilestodictionary();
            try
            {
                var Toprint = OrderDirectory.Where(d => d.Key > 0);
                foreach (var print in Toprint)
                {
                    Console.WriteLine($"Order Number: {print.Value.OrderNumber}\nEntry Key:{print.Key}\nCustomer name: {print.Value.CustomerName}, Order date {print.Value.OrderDate}\nProduct: {print.Value.Product}, Sales Zone: {print.Value.State}");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                }
            }
            catch
            {
                Console.WriteLine("Could not access or generate database, please contact IT");
                return;
            }
        }
        public static void InspectOrder()
        {
            UserIO.DisplayCurrentOrderFiles();
            OrderManager manager = new OrderManager();
            string orderdate = manager.PseudoDateSelect();
            string filename = "Orders_" + orderdate + ".txt";
            List<Order> CurrentOrders = new List<Order>();
            try
            {
                LoadOrders current = new LoadOrders();
                CurrentOrders = current.ParseFileData(filename);
            }
            catch
            {
                Console.WriteLine("Could not open specified file");
                return;
            }
            Console.Clear();
            Console.WriteLine($"Orders on {orderdate}");
            Console.WriteLine("Order#, Customer name, State, product type, area, total cost");
            if (CurrentOrders != null)
            {
                foreach (Order order in CurrentOrders)
                {
                    Console.WriteLine($"{order.OrderNumber}, {order.CustomerName}, {order.State}, {order.Product}, {order.Area}, {order.Total:C}");
                }

                Console.WriteLine("Enter the order number");
                int ordernumber = UserIO.GetIntegerFromUser();
                UserIO.DisplayOrder(filename, ordernumber);
            }
            else
            {
                Console.WriteLine("No orders to display\nPress any key to continue");
            }
            Console.ReadKey();
        }
        
    }
}
