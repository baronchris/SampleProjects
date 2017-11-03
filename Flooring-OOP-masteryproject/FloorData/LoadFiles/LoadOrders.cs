using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using FloorData;


namespace FloorData
{
    public class LoadOrders
    {
        public List<Order> ParseFileData(string filename)
        {
            List<Order> Orders = new List<Order>();
            List<string> lines = new List<string>();
            string orderfile = @"Orders\"+filename;
            if (File.Exists(@"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\"+orderfile))
            {
                try
                {
                    lines = FileIO.ReadWholeFilefromFileName(orderfile);

                }
                catch
                {
                    Console.WriteLine("Error: Could not access file system");
                    return null;
                }
                foreach (string line in lines)
                {
                    if (line != "")
                    {
                        string[] delimiters = new string[] { ","};
                        string[] fields = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                        for (int i=0; i<fields.Length; i++)
                        {
                           fields[i]=fields[i].Trim();
                            fields[i] = fields[i].Trim('"');
                        }
                        if (int.TryParse(fields[0], out int junk))
                        {
                            Order Temp = new Order(int.Parse(fields[0]), fields[1], fields[2], decimal.Parse(fields[3]), fields[4], decimal.Parse(fields[5]), decimal.Parse(fields[6]), decimal.Parse(fields[7]), decimal.Parse(fields[8]), decimal.Parse(fields[9]), decimal.Parse(fields[10]), decimal.Parse(fields[11]),filename);
                            Orders.Add(Temp);
                        }
                    }
                }
                
                return Orders;
            }
            else
            {
                Console.WriteLine("No orders found for this date");
                return null;
            }
        }
        public int GetHighestOrderNumber(List<string>files)
        {
            List<int> indexes = new List<int>();
            
            try
            {
                foreach (string file in files)
                {

                    List<int> TempList = new List<int>();
                    List<string> lines = new List<string>();
                    try//added, returns more complaints seems to still work
                    {
                        lines = FileIO.ReadWholeFilefromPath(file);
                        foreach (string line in lines)
                        {
                            if (line != "")
                            {
                                string[] fields = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                                if (int.TryParse(fields[0], out int junk))
                                {
                                    Order Temp = new Order(int.Parse(fields[0]), fields[1], fields[2], decimal.Parse(fields[3]), fields[4], decimal.Parse(fields[5]), decimal.Parse(fields[6]), decimal.Parse(fields[7]), decimal.Parse(fields[8]), decimal.Parse(fields[9]), decimal.Parse(fields[10]), decimal.Parse(fields[11]));
                                    TempList.Add(Temp.OrderNumber);
                                }
                            }
                        }
                        if (TempList.Count > 0)
                        {
                            indexes.Add(TempList.Max());
                        }
                        else
                        {
                            Console.WriteLine($"Could not read data in {file}");
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"could not access file{file}");
                        File.Delete(file);
                    }
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Warning: Could not read file all files: {indexes.Count} files read");
                Console.ForegroundColor = ConsoleColor.Green;
            }
            int Maxindex = indexes.Max();
            return Maxindex;
        }
        

        public Dictionary<int, Order>ParseOrderFilestodictionary()
        {
            Dictionary<int, Order> OrdersDictionary = new Dictionary<int, Order>();
            List<string> allfiles = FileIO.GetCurrentOrderFiles();
            List<Order> Orders = new List<Order>();
            try
            {
                foreach (string file in allfiles)
                {
                    List<string> lines = new List<string>();
                    lines = FileIO.ReadWholeFilefromPath(file);
                    foreach (string line in lines)
                    {
                        string sub = file.Substring(77, 8);
                        string filedate = sub.Substring(0, 2) + "/" + sub.Substring(2, 2) + "/" + sub.Substring(4, 4);

                        if (line != "")
                        {

                            string[] fields = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                            for (int i = 0; i < fields.Length; i++)
                            {
                                fields[i] = fields[i].Trim();
                                fields[i] = fields[i].Trim('"');
                            }
                            if (int.TryParse(fields[0], out int junk))
                            {
                                Order Temp = new Order(int.Parse(fields[0]), fields[1], fields[2], decimal.Parse(fields[3]), fields[4], decimal.Parse(fields[5]), decimal.Parse(fields[6]), decimal.Parse(fields[7]), decimal.Parse(fields[8]), decimal.Parse(fields[9]), decimal.Parse(fields[10]), decimal.Parse(fields[11]), filedate);
                                Orders.Add(Temp);
                            }
                        }
                    }

                }
            }
            catch
            {
                Console.WriteLine("Could not build Order database");
                return null;
            }
            List<Order> SortedOrders = Orders.OrderBy(o => o.OrderNumber).ToList();
            int k = 0;
            foreach (Order order in SortedOrders)
            {
                if (!OrdersDictionary.ContainsKey(order.OrderNumber))
                {
                    OrdersDictionary.Add(order.OrderNumber, order);
                }
                else if(OrdersDictionary.ContainsKey(order.OrderNumber))
                {
                    int key = order.OrderNumber + 100000 +k;
                    OrdersDictionary.Add(key, order);
                    k++;
                }
            }
            return OrdersDictionary;
            
        }
        public int GetHigestOrderNumberinFile(string path)
        {   

            List<string> lines = FileIO.ReadWholeFilefromPath(path);
            List<int> ordernumbers = new List<int>();
            foreach (string line in lines)
            {
                if (line != "")
                {

                    string[] fields = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    if (int.TryParse(fields[0], out int ordernumber))
                    {
                        ordernumbers.Add(ordernumber);
                    }
                }

            }
            if (ordernumbers.Count > 0)
            {
                return ordernumbers.Max();
            }
            else
            {
                Console.WriteLine($"could not parse {path}\nPress any key to continue");
               
                Console.ReadKey();
                return 0;
            }
        }
        public List<Order> ParseFileDataFromPath(string path)
        {
            List<Order> Orders = new List<Order>();
            List<string> lines = new List<string>();
            if (File.Exists(path))
            {
                try
                {
                    lines = FileIO.ReadWholeFilefromPath(path);

                }
                catch
                {
                    Console.WriteLine("Error: Could not access file system");
                    return null;
                }
                foreach (string line in lines)
                {
                    if (line != "")
                    {
                        string[] delimiters = new string[] { "," };
                        string[] fields = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                        for (int i = 0; i < fields.Length; i++)
                        {
                            fields[i] = fields[i].Trim();
                            fields[i] = fields[i].Trim('"');
                        }
                        if (int.TryParse(fields[0], out int junk))
                        {
                            Order Temp = new Order(int.Parse(fields[0]), fields[1], fields[2], decimal.Parse(fields[3]), fields[4], decimal.Parse(fields[5]), decimal.Parse(fields[6]), decimal.Parse(fields[7]), decimal.Parse(fields[8]), decimal.Parse(fields[9]), decimal.Parse(fields[10]), decimal.Parse(fields[11]));
                            Orders.Add(Temp);
                        }
                    }
                }

                return Orders;
            }
            else
            {
                Console.WriteLine("No orders found for this date");
                return null;
            }
        }

    }
}
