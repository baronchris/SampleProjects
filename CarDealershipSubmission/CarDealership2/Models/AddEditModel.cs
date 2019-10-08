using CarDealership.Data;
using CarDealership.Data.carcomponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership2.Models
{
    public class AddEditModel
    {
        public List<SelectListItem> MakeChoices;
       
        public CarModels ModelToAdd;
        public List<CarModels> CurrentModels;
        [Required (ErrorMessage ="please enter a model name")]
        [DisplayName("Model Name")]
        public string ModelName { get; set; }
        [Required(ErrorMessage = "please select a make")]
        [DisplayName("Make")]
        public int MakeID { get; set; }

        public void SetMakeChoices()
        {
            CarComponentRepoADO repoc = new CarComponentRepoADO();
            List<Makes> makes = repoc.GetMakes();
            List<SelectListItem> m = new List<SelectListItem>();
            foreach(Makes make in makes)
            {
                m.Add(new SelectListItem()
                {
                    Value = make.MakeID.ToString(),
                    Text = make.MakeName,
                    Selected = false
                });
            }
            MakeChoices = m;
            List<CarModels> cm = repoc.GetModels();
            CurrentModels = cm;
        }
    }
}