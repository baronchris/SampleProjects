using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
    public interface IPromotionRepo
    {
        Promotion GetPromotionByDate(DateTime Promotiondate);
        Promotion GetPromotionByID(int ID);
        void AddPromotion(Promotion promo);
        List<Promotion> GetALLpromotions();
        List<Promotion> GetValidPromotions();
        void DeletePromotion(int ID);
    }
}
