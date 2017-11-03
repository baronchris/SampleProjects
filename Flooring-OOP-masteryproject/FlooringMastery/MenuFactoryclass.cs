using FlooringMastery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlooringMastery
{
   public static class MenuFactoryclass
    {
        public static IMenuSelector  Create()
        {
            Console.WriteLine("Please enter your username");
            string username = Console.ReadLine().ToUpper();
            switch (username)
            {
                case "ADMIN":
                    return new AdminMenu();
                default:
                    return new Menu();
            }
        }
    }
}
