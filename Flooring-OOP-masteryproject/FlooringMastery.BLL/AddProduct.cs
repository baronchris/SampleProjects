using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
   public static class AddProduct
    {
        public static void AddNewProduct()
        {
            Console.WriteLine("Press any key to add a new product or enter \"N\" to exit");
            string input = Console.ReadLine();
            if (input.ToUpper() == "N")
            {
                Console.WriteLine("No product added\nPress any key to continue");
                Console.ReadKey();
                return;
            }
            OrderManager o = new OrderManager();
            Console.WriteLine("Set the new product's name");
            string Name=o.GetName();
            Console.WriteLine("Set the new product's Material cost per square foot");
            decimal matCost = o.GetCost();
            Console.WriteLine("Set the new product's Labor cost per square foot");
            decimal labCost = o.GetCost();
            Product newlyAdded = new Product(Name, matCost, labCost);
            string filename = "Products" + DateTime.Today.ToString("MMddyyyy") + ".txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(@"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\Products\" + filename))
                {
                    sw.WriteLine($"{newlyAdded.ProductName},{newlyAdded.CostperSquare},{newlyAdded.LaborCost}");
                }
            }
            catch
            {

            }
            //Try write to order file ,true
        }
    }
}
