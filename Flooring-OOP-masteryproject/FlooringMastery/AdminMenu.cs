using FlooringMastery.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Models;
using FlooringMastery.Interfaces;

namespace FlooringMastery
{
    public class AdminMenu:IMenuSelector
    {
        public void Start()
        {
            bool keepGoing = true;
            while (keepGoing == true)
            {
                Console.Clear();
                Console.WriteLine("SG Flooring Order Administration\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("---------------------------");
                Console.WriteLine("***************************");
                Console.WriteLine("System Administration Menu");
                Console.WriteLine("***************************");
                Console.WriteLine("---------------------------\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1: Display an Order");
                Console.WriteLine("2: Add an Order");
                Console.WriteLine("3: Edit an Order");
                Console.WriteLine("4: Remove an Order");
                Console.WriteLine("5: Display all orders for a given date");
                Console.WriteLine("6: Display and manage products"); // make submenu
                Console.WriteLine("7: Display and manage Sales Zones");//upgrade to add states  use submenu 
                Console.WriteLine("8: Display all orders from file directroy");//upgrade to write centralized db file
                Console.WriteLine("9: Order database management"); //add in read files from database display order by key or search by order number to get key
                Console.WriteLine("10: Quit");
                Console.WriteLine("----------------------------------");
                
                Console.WriteLine("Please enter a selection");
                int userInt = UserIO.GetIntegerFromUser();
                OrderManager manager = new OrderManager();
                switch (userInt)
                {
                    
                    case 1:
                        UserIO.InspectOrder();
                        break;
                    case 2:
                        manager.AddOrder();
                        Console.ReadKey();
                        break;
                    case 3:
                        UserIO.DisplayCurrentOrderFiles();
                        manager.EditOrder();
                        break;
                    case 4:
                        UserIO.DisplayCurrentOrderFiles();
                        manager.RemoveOrder();
                        break;
                    case 5:
                        UserIO.DisplayCurrentOrderFiles();
                        string orderdate = manager.DateSelectView();
                        string filename = "Orders_" + orderdate + ".txt";
                        UserIO.PrintOrders(filename);
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();
                        UserIO.DisplayProducts();
                        AddProduct.AddNewProduct();
                        Console.ReadKey();
                        break;
                    case 7:
                        Console.Clear();
                        UserIO.DisplayStates();
                        Console.ReadKey();
                        break;
                    case 8:
                        Console.Clear();
                        UserIO.Displaydictionary();
                        Console.ReadKey();
                        break;
                    case 9:
                        Console.Clear();
                        DatabaseManager.Execute();
                        Console.ReadKey();
                        break;
                    case 10:
                        keepGoing = false;
                        break;
                    default:
                        Console.WriteLine("please select a menu option\nPress any key to continue");
                        Console.ReadKey();
                        break;
                }

            }
            
        } 
    }
}
