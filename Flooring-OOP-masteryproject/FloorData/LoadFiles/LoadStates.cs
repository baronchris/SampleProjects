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
    public static class LoadStates
    {
        public static List<State> ParseStateData()
        {
            List<string> lines = new List<string>();
            List<State> States = new List<State>();
            try
            {
                lines = FileIO.ReadWholeFilefromFileName(@"Taxes\Taxes.txt");
            }
            catch
            {
                Console.WriteLine("Could not acces the State Tax database.  Please contact IT");
                return null;
            }
            if (lines != null)
            {
                foreach (string line in lines)
                {
                    string[] fields = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    if (decimal.TryParse(fields[2], out decimal junk))
                    {
                        State Temp = new State(fields[0], fields[1], decimal.Parse(fields[2]));
                        States.Add(Temp);
                    }
                }

                return States;
            }
            else
            {
                Console.WriteLine("Could not load product list");
                return null;
            }
        }
        public static List<State> LoadAllStates()
        {
            List<State> States = new List<State>();
            List<string> allfiles = System.IO.Directory.GetFiles(@"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\Taxes").ToList();
            foreach (string file in allfiles)
            {

                List<string> lines = new List<string>();
                lines = FileIO.ReadWholeFilefromPath(file);
                foreach(string line in lines)
                {
                    if (line != "")
                    {
                        string[] fields = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                        for (int i = 0; i < fields.Length; i++)
                        {
                            fields[i] = fields[i].Trim();
                        }
                        if (decimal.TryParse(fields[2], out decimal junk))
                        {
                            State Temp = new State(fields[0], (fields[1]), decimal.Parse(fields[2]));

                            State Tempcheck = States.Where(s => s.StateCode == Temp.StateCode).FirstOrDefault();
                            if (Tempcheck == null)
                            {
                                States.Add(Temp);
                            }
                        }
                    }
                }
            }
            return States;

        }
    }
}
