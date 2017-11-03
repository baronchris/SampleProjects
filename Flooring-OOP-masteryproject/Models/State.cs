using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class State
    {
       public string StateCode { get; set; }
       public string StateName { get; set; }
        
       public decimal TaxRate { get; set; }

        public State(string statecode, string statename, decimal tax)
        {
            StateCode = statecode;
            StateName = statename;
            TaxRate = tax;
        }
    }
}
