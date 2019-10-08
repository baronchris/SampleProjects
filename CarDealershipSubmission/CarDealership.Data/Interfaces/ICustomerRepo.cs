using CarDealership.Data.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface ICustomerRepo
    {
        List<State> GetStates();
        List<PaymentType> GetPaymentTypes();
       
    }
}
