using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorData;


namespace FlooringMastery.BLL
{
   public class OrderManager
    {
        public string PseudoDateSelect()
        {
            bool goodTry = false;
            string result = "";
            while (!goodTry)
            {
                Console.WriteLine("Please enter the file date you wish to view in the format mm/dd/yyyyy");
                string userInput = Console.ReadLine();
                if (!userInput.Contains("/"))
                {
                    Console.WriteLine("Please use the format mm/dd/yyyyy");
                }
                else
                {
                    string[] splitdate = userInput.Split('/');
                    if (splitdate.Length == 3 && splitdate[0].Length==2 && splitdate[1].Length == 2 && splitdate[2].Length ==4)
                    {
                        result= splitdate[0] + splitdate[1] + splitdate[2];
                        goodTry = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please use the format mm/dd/yyyyy");
                    }
                }
            }
            return result; 
        }

        public string DateSelect()
        {
            bool goodDate = false;
            DateTime orderdate = DateTime.Now;
            while (!goodDate)
            {
                Console.WriteLine("Please enter a date for the new order in the following format:MM/DD/YYYY.\nDates must be in the future");
                string dateinput = Console.ReadLine();
                
                bool validdate= DateTime.TryParse(dateinput, out orderdate);
                if (orderdate.CompareTo(DateTime.Today) < 0)
                {
                    Console.WriteLine("The date must be later than today");
                    goodDate = false;
                }
                else if(validdate=true && orderdate.CompareTo(DateTime.Today) >= 0)
                {
                    goodDate = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid date");
                }
            }
            string filedate = orderdate.ToString("MMddyyyy");
            return filedate;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        public string DateSelectView()
        {
            bool goodDate = false;
            DateTime orderdate = DateTime.Now;
            while (!goodDate)
            {
                Console.WriteLine("Please enter a date for the new order in the following format:MM/DD/YYYY");
                string dateinput = Console.ReadLine();

                bool validdate = DateTime.TryParse(dateinput, out orderdate);
                if (validdate == true)
                {
                    goodDate = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid date");
                }
            }
            string filedate = orderdate.ToString("MMddyyyy");
            return filedate;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        public void AddOrder()
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
            List<State> States = new List<State>();
            try
            {
                States = LoadStates.LoadAllStates();
            }
            catch
            {
                Console.WriteLine("Could not load sales tax data");
            }

            int ordernumber = FileIO.ReturnHighestOrderNumber() + 1;
            string date = DateSelect();
            string filename = "Orders_" + date + ".txt";
            string path = @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\Orders\" + filename;
            List<Order> CurrentOrders = new List<Order>();
            if (File.Exists(path))
            {
                LoadOrders current = new LoadOrders();
                CurrentOrders = current.ParseFileData(filename);
                
            }
            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                }
            }
            string name = GetName();
            Console.Clear();
            Console.WriteLine("Please enter the number associated with the customer's sales zone");
            UserIO.DisplayStates();
            State state = GetStateByIndex(States);
            Console.Clear();
            UserIO.DisplayProducts();
            Product myProduct = GetProductByIndex(Products);
            decimal area = GetArea();
            Order MyOrder = new Order(ordernumber, name, state, myProduct, area, date);
            Console.WriteLine($"The order for {name} in {state.StateName} is {area}ft^2 of {myProduct.ProductName}");
            Console.WriteLine($"The cost/ft^2 for material is{MyOrder.CostPerSquareFoot}, and the labor cost/ft^2 is{MyOrder.LaborCostPerSquareFoot}");
            Console.WriteLine($"Total material cost: {MyOrder.MaterialCost:C} \nTotal labor cost: {MyOrder.LaborCost:C}\nTaxes: {MyOrder.Tax:C}\nTotal cost: {MyOrder.Total:C}");
            Console.WriteLine("Do you wish to submit this order?  Enter \"n\" to discard order");
            string input = Console.ReadLine().ToUpper();
            if(input != "N")
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine($"{MyOrder.OrderNumber},{MyOrder.CustomerName},{MyOrder.State},{MyOrder.TaxRate},{MyOrder.Product},{MyOrder.Area},{MyOrder.CostPerSquareFoot},{MyOrder.LaborCostPerSquareFoot},{MyOrder.MaterialCost},{MyOrder.LaborCost},{MyOrder.Tax},{MyOrder.Total}");
                    }
                }
                catch
                {
                    Console.WriteLine("System Error: order not submitted");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Order terminated");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Order entry complete");
            
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        public void EditOrder()
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
            List<State> States = new List<State>();
            try
            {
                States = LoadStates.LoadAllStates();
            }
            catch
            {
                Console.WriteLine("Could not load sales tax data");
            }
            string date = PseudoDateSelect();
            string filename = "Orders_" + date + ".txt"; //enusre write-ability
            string path = @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\Orders\" + filename;
            List<Order> CurrentOrders = new List<Order>();
            if (!File.Exists(path))
            {
                Console.WriteLine("Invalid date");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }
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
            Console.WriteLine($"Orders on {date}");
            Console.WriteLine("Order#, Customer name, State, product type, area, total cost");
            foreach(Order order in CurrentOrders)
            {
                Console.WriteLine($"{order.OrderNumber}, {order.CustomerName}, {order.State}, {order.Product}, {order.Area}, {order.Total:C}");
            }
            
            bool GoodOrder = false;
            Order OrderToEdit = new Order();
            while (!GoodOrder)
            {
                Console.WriteLine("Please enter the order number of the order you wish to edit");
                int selection = UserIO.GetIntegerFromUser();
                Order chosen = CurrentOrders.Where(o => o.OrderNumber == selection).FirstOrDefault();
                if (chosen != null)
                {
                    OrderToEdit = chosen;
                    GoodOrder = true;
                    break;
                }
                else { Console.WriteLine("Enter a valid order number"); }
            }
            Console.WriteLine($"The current Customer name is {OrderToEdit.CustomerName}");
            Console.WriteLine("Enter a new name, or leave the field blank to retain the current name");
            string namechange = Console.ReadLine();  // will need this to be better, maybe seperate into change name function

            if (namechange != "")
            {
                if (namechange[0].ToString() != " ")
                {
                    OrderToEdit.CustomerName = namechange;
                    Console.WriteLine($"Customer name changed to {OrderToEdit.CustomerName}");
                }
                else
                {
                    Console.WriteLine($"Customer name reamins {OrderToEdit.CustomerName}");
                }
            }
            else
            {
                Console.WriteLine($"Customer name remains {OrderToEdit.CustomerName}");
            }
            Console.WriteLine($"The current state is {OrderToEdit.State}");
            
            UserIO.DisplayStates();
            Console.WriteLine("Please enter the number associated with the customer's updated state");
            string statechange = Console.ReadLine();
            State Mystate = null;
            if (statechange != "")
            {
                if(statechange[0].ToString()!= " ")
                {
                    
                    
                    Mystate = EditState(States, statechange);
                    OrderToEdit.State = Mystate.StateCode;
                    OrderToEdit.TaxRate = Mystate.TaxRate;
                    
                }
                else
                {
                    Console.WriteLine($"State remains {OrderToEdit.State}");
                    Mystate = GetState(OrderToEdit.State, States);
                }
            }
            else
            {
                Console.WriteLine($"State remains {OrderToEdit.State}");
                Mystate = GetState(OrderToEdit.State, States);
            }
            Console.Clear();
            UserIO.DisplayProducts();
            Console.WriteLine($"The current product selected is {OrderToEdit.Product}");
            Console.WriteLine("please enter the new product, or leave the field empty to continue");
            Product changedproduct = null;
            string productchange = Console.ReadLine();
            if (productchange != "")
            {
                if(productchange[0].ToString()!=" ")
                {
                    bool isFinished = false;
                    int productchangeIndex = 0;
                    while (!isFinished)
                    {
                        
                        bool goodInt = int.TryParse(productchange, out productchangeIndex);

                        if (goodInt)
                        {
                            changedproduct = GetProductByIndex(productchangeIndex, Products);
                            OrderToEdit.Product = changedproduct.ProductName;
                            OrderToEdit.LaborCostPerSquareFoot = changedproduct.LaborCost;
                            OrderToEdit.CostPerSquareFoot = changedproduct.CostperSquare;
                            Console.WriteLine($"The product has been updated to {OrderToEdit.Product} with a material cost of of {OrderToEdit.CostPerSquareFoot:C}/ft^2, \nand a labor cost of {OrderToEdit.LaborCostPerSquareFoot:C}");
                            isFinished = true;
                        }
                        else
                        {
                            Console.WriteLine($"Please enter a valid index number in the range of 1 to {Products.Count}");
                            productchange = Console.ReadLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Product remains {OrderToEdit.Product}");
                    changedproduct = GetMyProduct(OrderToEdit.Product, Products);
                }
            }
            else
            {
                Console.WriteLine($"Product remains {OrderToEdit.Product}");
                changedproduct = GetMyProduct(OrderToEdit.Product, Products);
            }
            Console.WriteLine($"The current area is {OrderToEdit.Area}");
            Console.WriteLine("please enter the new area, or leave the field empty to continue");
            string areachange = Console.ReadLine();
            if (areachange != "")
            {
                if (areachange[0].ToString() != " ")
                {
                    OrderToEdit.Area = GetArea(areachange);
                    Console.WriteLine($"The new area is {OrderToEdit.Area}");
                }
                else
                {
                    Console.WriteLine($"The area remains {OrderToEdit.Area}");
                }
            }
            else
            {
                Console.WriteLine($"The area remains {OrderToEdit.Area}");
            }
            OrderToEdit.ComputOrderValues(changedproduct, Mystate);
            Console.WriteLine($"The order now stands at:\n{ OrderToEdit.CustomerName}, in {OrderToEdit.State}, for {OrderToEdit.Area}ft^2 {OrderToEdit.Product} for a total of {OrderToEdit.Total:C}");
            Console.WriteLine("Do you wish to save these these changes?");
            Console.WriteLine("enter \'N\' to discard changes or any other key to keep changes?");
            string SaveChoice=Console.ReadLine().ToUpper();
            if (SaveChoice == "N")
            {
                Console.WriteLine("Changes Discared");
                return;
            }
            else
            {
                              
                try
                {
                    using(StreamWriter sw =new StreamWriter(path))
                    {
                        sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                        foreach(Order MyOrder in CurrentOrders)
                        {
                            sw.WriteLine($"{MyOrder.OrderNumber},\"{MyOrder.CustomerName}\",{MyOrder.State},{MyOrder.TaxRate},{MyOrder.Product},{MyOrder.Area},{MyOrder.CostPerSquareFoot},{MyOrder.LaborCostPerSquareFoot},{MyOrder.MaterialCost},{MyOrder.LaborCost},{MyOrder.Tax},{MyOrder.Total}");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Could not write updated order to file");
                    return;
                }
                Console.WriteLine("Order edited");
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        public void RemoveOrder()
        {
            string date = PseudoDateSelect();
            string filename = "Orders_" + date + ".txt";
            string path = @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\Orders\" + filename;
            List<Order> CurrentOrders = new List<Order>();
            if (!File.Exists(path))
            {
                Console.WriteLine("Invalid date");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }
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
            Console.WriteLine($"Orders on {date}");
            Console.WriteLine("Order#, Customer name, State, product type, area, total cost");
            foreach (Order order in CurrentOrders)
            {
                Console.WriteLine($"{order.OrderNumber}, {order.CustomerName}, {order.State}, {order.Product}, {order.Area}, {order.Total:C}");
            }
            bool GoodOrder = false;
            int todelete = 0;
            int maxOrderNumber = CurrentOrders[CurrentOrders.Count - 1].OrderNumber;
            while (!GoodOrder)
            {
                Console.WriteLine("Please enter the order number of the order you wish to delete");
                int selection = UserIO.GetIntegerFromUser();
                if (selection > maxOrderNumber || selection < 1)
                {
                    GoodOrder = false;
                    Console.WriteLine("Please enter a valid order number");
                }
                else
                {
                    GoodOrder = true;
                    todelete = selection;
                    break;
                }
            }
            foreach(Order order in CurrentOrders)
            {
                if (order.OrderNumber == todelete)
                {
                    CurrentOrders.Remove(order);
                    break;
                }
            }
            if (CurrentOrders.Count<1)
            {
                File.Delete(path);
                Console.WriteLine("Date file deleted");
            }
            else
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                        foreach (Order MyOrder in CurrentOrders)
                        {
                            sw.WriteLine($"{MyOrder.OrderNumber},\"{MyOrder.CustomerName}\",{MyOrder.State},{MyOrder.TaxRate},{MyOrder.Product},{MyOrder.Area},{MyOrder.CostPerSquareFoot},{MyOrder.LaborCostPerSquareFoot},{MyOrder.MaterialCost},{MyOrder.LaborCost},{MyOrder.Tax},{MyOrder.Total}");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Could not write updated order to file");
                    return;
                }
            }
            Console.WriteLine("Order deleted");
            Console.ReadLine();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------ 
        public decimal GetArea()
        {
            decimal area = 0;
            bool isValidArea = false;
            while (!isValidArea)
            {
                Console.WriteLine("Please enter the area of flooring needed");
                string input = Console.ReadLine();
                isValidArea = decimal.TryParse(input, out area);
                if (area < 100)
                {
                    Console.WriteLine("Area must be at least 100ft^2");
                    isValidArea = false;
                }
                if(isValidArea==true && area >= 100)
                {
                    return area;
                }
                if (!isValidArea)
                {
                    Console.WriteLine("Please enter a valid area");
                    input = Console.ReadLine();
                }
            }
            return 0;
        }
        public State GetState(string input, List<State> states) //overload for edit
        {

            bool goodState = false;
            while (!goodState)
            {
                string state = input.ToUpper();
                if (state.Length > 3)
                {
                    State chosen = states.Where(s => s.StateName.ToString().ToUpper().Trim() == state).FirstOrDefault();

                    if (chosen != null)
                    {
                        goodState = true;

                        return chosen;

                    }
                    else
                    {
                        Console.WriteLine("Please ensure you've entered a valid state name or abbreviation and that your state has been entered into our system");
                        state = Console.ReadLine().ToUpper();
                    }

                }
                else if (state.Length == 2|| state.Length==3)
                {
                    State chosen = states.Where(s => s.StateCode.ToString().ToUpper().Trim() == state).FirstOrDefault();

                    if (chosen != null)
                    {
                        goodState = true;

                        return chosen;

                    }
                    else
                    {
                        Console.WriteLine("Please ensure you've entered a valid state name or abbreviation and that your state has been entered into our system");
                    }
                }
                else
                {
                    Console.WriteLine("Please ensure you've entered a valid state name or abbreviation and that your state has been entered into our system");
                }
            }
            return null;
        }
        public Product GetMyProduct(string userinput, List<Product> Products)//overload for edit
        {
            bool realProduct = false;
            while (!realProduct)
            {
                string input = userinput.ToUpper();
                Product chosen = Products.Where(p => p.ProductName.ToUpper().Trim() == input).FirstOrDefault();
                if (chosen != null)
                {
                    realProduct = true;
                    return chosen;
                }
                else
                {
                    Console.WriteLine("please enter a valid product name");
                    input = Console.ReadLine().ToUpper();
                }
            }
            return null;
        }
        public decimal GetArea(string areainput)//overload for edit
        {
            decimal area = 0;
            string input = areainput;
            bool isValidArea = false;
            while (!isValidArea)
            {
                isValidArea = decimal.TryParse(input, out area);
                if (area < 100)
                {
                    Console.WriteLine("Area must be at least 100ft^2");
                    isValidArea = false;
                    Console.WriteLine("Please enter the area of flooring needed");
                    input = Console.ReadLine();
                }
                if (isValidArea == true && area >= 100)
                {
                    return area;
                }
                if (!isValidArea)
                {
                    Console.WriteLine("Please enter a valid area");
                    input = Console.ReadLine();
                }
            }
            return 0;
        }
        public string GetName()
        {
            string Name=null;
            bool goodname = false;
            while (!goodname)
            {
                Console.WriteLine("Please enter the name");
                string potentialName = Console.ReadLine();
                if (potentialName != "")
                {
                    if (potentialName[0].ToString() != " ")
                    {
                        Name = potentialName;
                        goodname = true;
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid name");
                    }
                }
                else
                {
                    Console.WriteLine("That is not a valid name");
                }
            }
            Name = "\"" + Name + "\"";
            return Name;
        }
        public string GetName(string UserInput)
        {

            string Name = null;
            bool goodname = false;
            string potentialName = UserInput;
            while (!goodname)
            {
                if (potentialName != "")
                {
                    if (potentialName[0].ToString() != " ")
                    {
                        Name = potentialName;
                        goodname = true;
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid name");
                        Console.WriteLine("Please enter the name of the customer");
                        potentialName = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("That is not a valid name");
                    Console.WriteLine("Please enter the name of the customer");
                    potentialName = Console.ReadLine();
                }
            }
            Name = "\"" + Name + "\"";
            return Name;
        }
        public decimal GetCost()
        {
            bool GoodDecimal = false;
            decimal output = 0;
            while (!GoodDecimal)
            {
                Console.WriteLine("Please enter the cost per square foot");
                string input = Console.ReadLine();
                decimal temp = 0;
                bool usable = decimal.TryParse(input, out temp);
                if(usable && temp > 0)
                {
                    output = temp;
                    GoodDecimal = true;
                }
            }
            return output;
        }
        public Product GetProductByIndex(List<Product> Products)
        {
            bool realProduct = false;
            while (!realProduct)
            {
                Console.WriteLine("Enter the number of the product you wish to select");
                int userchoice= UserIO.GetIntegerFromUser()-1;
                if(userchoice<Products.Count() && userchoice >= 0)
                {
                    Product myproduct = Products[userchoice];
                    return myproduct;
                }
            }
            return null;
        }
        public State GetStateByIndex(List<State> States)
        {
            bool realState = false;
            while (!realState)
            {
                Console.WriteLine("Enter the number of the sales zone you wish to select");
                int userchoice = UserIO.GetIntegerFromUser() - 1;
                if(userchoice<States.Count && userchoice >= 0)
                {
                    State myState = States[userchoice];
                    
                    realState=true;
                    return myState;
                }
                else
                {
                    Console.WriteLine("Please select a valid zone");
                    realState = false;
                }
            }
            return null;
        }
        public State GetStateByIndex(List<State> States, int userinput)
        {
            bool realState = false;
            int userchoice = userinput - 1;
            while (!realState)
            {
                
                if (userchoice < States.Count && userchoice >= 0)
                {
                    State myState = States[userchoice];
                    return myState;
                }
                else
                {
                    Console.WriteLine("Enter the number of the product you wish to select");
                    userchoice = UserIO.GetIntegerFromUser() - 1;
                }
            }
            return null;
        }
        public Product GetProductByIndex(int productindex, List<Product> Products)
        {
            bool realProduct = false;
            int userchoice = productindex-1;
            while (!realProduct)
            {
                Console.WriteLine("Enter the number of the product you wish to select");
                
                if (userchoice < Products.Count() && userchoice >= 0)
                {
                    Product myproduct = Products[userchoice];
                    return myproduct;
                }
                else {
                    Console.WriteLine("Invalid selection");
                    userchoice = UserIO.GetIntegerFromUser() - 1;
                }
            }
            return null;
        }
        public State EditState(List<State> states, string userinput)
        {
            int userIntinput = 0;
            bool validInt = int.TryParse(userinput, out userIntinput);
            if(validInt)
            {
                if (userIntinput - 1 < states.Count)
                {
                    return states[userIntinput - 1];
                }
                else
                {
                    Console.WriteLine("Please select a valid zone");
                    State myState = GetStateByIndex(states);
                    return myState;
                }
            }
            else
            {
                Console.WriteLine("Please select a valid zone");
                State myState = GetStateByIndex(states);
                return myState;
            }
            
        }
    }
}
