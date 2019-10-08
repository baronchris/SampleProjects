using CarDealership.Data.carcomponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IComponentRepo
    {
        List<BodyStyles> GetBodies();
        List<CarModels> GetModels();
        List<Makes> GetMakes();
        List<Colors> GetColors();
        List<Transmission> GetTrans();
        List<Interiors> GetInteriors();
    }
}
