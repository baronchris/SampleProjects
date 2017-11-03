
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace FloorData
{
    public static class FileIO
    {
        const string productPath = @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\Products.txt";
        const string statePath = @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\Taxes.txt";

        public static List<string> GetFileData(string filename)
        {
            string directory = @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\";
            string path = directory + filename;
            List<string> rows = new List<string>();
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            line = sr.ReadLine();
                            if (line != null)
                            {
                                rows.Add(line);
                            }
                        }

                    }
                }
                catch
                {
                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not acces file data.  Please contact IT");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                return rows;
            }
            else
            {
                Console.WriteLine("File does not exist");
                return null;
            }
        }
        public static List<string> GetFileDataforIndex(string filename)
        {

            string path = filename;
            List<string> rows = new List<string>();
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            line = sr.ReadLine();
                            if (line != null)
                            {
                                rows.Add(line);
                            }
                        }

                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Could not acces file data at{path}.  Please contact IT");
                    Console.ReadKey();
                    Console.ForegroundColor = ConsoleColor.Green;
                    return null;
                }
                return rows;
            }
            else
            {
                Console.WriteLine("File does not exist");
                return null;
            }
        }
        public static List<string> GetFileDatafromPath(string filename)
        {

            string path = filename;
            List<string> rows = new List<string>();
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            line = sr.ReadLine();
                            if (line != null)
                            {
                                rows.Add(line);
                            }
                        }

                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not acces file data.  Please contact IT");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                return rows;
            }
            else
            {
                Console.WriteLine("File does not exist");
                return null;
            }
        }
        public static List<string> ReadWholeFilefromPath(string filename)
        {

            string path = filename;
            List<string> rows = new List<string>();
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line;
                        do
                        {
                            line = sr.ReadLine();
                            if (line != null)
                            {
                                rows.Add(line);
                            }
                        } while (line != null);

                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not acces file data.  Please contact IT");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                return rows;
            }
            else
            {
                Console.WriteLine("File does not exist");
                return null;
            }
        }
        public static List<string> ReadWholeFilefromFileName(string filename)
        {

            string path = @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\"+filename;
            List<string> rows = new List<string>();
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line;
                        do
                        {
                            line = sr.ReadLine();
                            if (line != null)
                            {
                                rows.Add(line);
                            }
                        } while (line != null);

                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not acces file data.  Please contact IT");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                return rows;
            }
            else
            {
                Console.WriteLine("File does not exist");
                return null;
            }
        }
        public static void WriteOrdersDatabase()
        {
            LoadOrders o = new LoadOrders();
            Dictionary<int, Order> OrderDirectory = o.ParseOrderFilestodictionary();
            string OrderDatabase = DateTime.Today.ToString("MMddyyyy") + ".txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(@"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\" + OrderDatabase))
                {
                    sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total,OrderDate");
                    var Toprint = OrderDirectory.Where(d => d.Value.OrderNumber > 0);
                    foreach (var print in Toprint)
                    {
                        sw.WriteLine($"{print.Value.OrderNumber.ToString()},{print.Key},{print.Value.CustomerName},{print.Value.State},{print.Value.TaxRate},{print.Value.Product},{print.Value.Area.ToString()},{print.Value.CostPerSquareFoot.ToString()},{print.Value.LaborCostPerSquareFoot.ToString()},{print.Value.MaterialCost.ToString()},{print.Value.LaborCost.ToString()},{print.Value.Tax.ToString()},{print.Value.Total.ToString()},{print.Value.OrderDate}");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Could not create database File");
                return;
            }
            Console.WriteLine("Database file created");
        }
        public static List<string> GetCurrentOrderFiles()
        {
            List<string> allfiles = System.IO.Directory.GetFiles(@"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\Orders").ToList();
            List<string> GoodFiles = new List<string>();
           foreach(string file in allfiles)
            {
                string Mimetype = System.Web.MimeMapping.GetMimeMapping(file); //Must use assembly ref for system.web in refs
                if ( Mimetype=="text/plain")
                {
                GoodFiles.Add(file);
                }
               /* else
                {
                    File.Move(file, @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\badorders\"); need to have specific file name
                }*/
            }
            Console.WriteLine($"all files count{allfiles.Count}, Valid files {GoodFiles.Count}\nPress any key to continue");
            Console.ReadLine();
            return GoodFiles; 
        }
        public static int ReturnHighestOrderNumber()
        {
            List<string> allfiles = GetCurrentOrderFiles();
            LoadOrders o = new LoadOrders();
            int highest = o.GetHighestOrderNumber(allfiles);
            return highest;
        }

    }
}
    
