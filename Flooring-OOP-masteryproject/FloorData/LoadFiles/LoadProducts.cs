using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FloorData;

namespace FloorData
{
   public static class LoadProducts
    {
        public static List<Product> ParseProductData()
        {
            List<string> lines = new List<string>();
            List<Product> Products = new List<Product>();
            try
            {
             lines = FileIO.GetFileData(@"Products\Products.txt");
            }
            catch
            {
                Console.WriteLine("Could not acces the products database.  Please contact IT");
                return null;
            }
            if(lines != null)
            {
                foreach (string line in lines)
                {
                    string[] fields = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    Product Temp = new Product(fields[0], decimal.Parse(fields[1]), decimal.Parse(fields[2]));
                    Products.Add(Temp);
                }

                return Products;
            }
            else
            {
                Console.WriteLine("Could not load product list");
                return null;
            }
        }
        public static List<Product> LoadAllProducts()
        {
            List<string> allfiles = System.IO.Directory.GetFiles(@"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\Products").ToList();
            List<Product> Products = new List<Product>();
            foreach (string file in allfiles)
            {
                List<string> lines = new List<string>();
                lines = FileIO.ReadWholeFilefromPath(file);
                foreach (string line in lines)
                {
                    if (line != "")
                    {
                        string[] fields = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                        for(int i=0; i<fields.Length;i++)
                        {
                            fields[i]=fields[i].Trim();
                            fields[i] = fields[i].Trim('"');

                        }
                        if (decimal.TryParse(fields[2], out decimal junk))
                        {
                            Product Temp = new Product(fields[0], decimal.Parse(fields[1]), decimal.Parse(fields[2]));

                            Product Tempcheck = Products.Where(p => p.ProductName == Temp.ProductName).FirstOrDefault();
                            if (Tempcheck == null)
                            {
                                Products.Add(Temp);
                            }

                        }
                    }
                }
            }
            return Products;
        }

    }
}
