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
    public class Menu:IMenuSelector
    {
        public void Start()
        {
            bool keepGoing = true;
            while (keepGoing == true)
            {
                Console.Clear();
                Console.WriteLine("SG Flooring Order Administration");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1: Display an Order");
                Console.WriteLine("2: Add an Order");
                Console.WriteLine("3: Edit an Order");
                Console.WriteLine("4: Remove an Order");
                Console.WriteLine("5: Quit");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Please enter a selection");
                int userInt = UserIO.GetIntegerFromUser();
                OrderManager manager = new OrderManager();
                switch (userInt)
                {
                    
                    case 1:
                        UserIO.InspectOrder();
                        UserIO.DisplayCurrentOrderFiles();
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
