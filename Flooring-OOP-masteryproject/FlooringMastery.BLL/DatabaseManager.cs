using FloorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.BLL
{
    public static class DatabaseManager
    {
        public static void Execute()
        {
            bool leave = false;
            while (!leave)
            {
                Console.WriteLine("Database Manager");
                Console.WriteLine("alpha test version");
                Console.WriteLine("1: Display order directory");
                Console.WriteLine("2: Write order directory to file");
                Console.WriteLine("3: exit to main menu");
                Console.WriteLine("Please enter a selection");
                int userInt = UserIO.GetIntegerFromUser();
                switch (userInt)
                {
                    case 1:
                        Console.Clear();
                        UserIO.Displaydictionary();
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        UserIO.Displaydictionary();
                        FileIO.WriteOrdersDatabase();
                        Console.ReadKey();
                        break;
                    case 3:
                        leave = true;
                        break;
                }
            }

        }
    }
}
