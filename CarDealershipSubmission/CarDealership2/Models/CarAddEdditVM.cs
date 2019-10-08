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
    public class CarAddEdditVM
    {
   
        public List<SelectListItem> ModelChoices { get; set; }
     
        public List<SelectListItem> MakeChoices { get; set; }
       
        public List<SelectListItem> ColorChoices { get; set;}
       
        public List<SelectListItem> InteriorChoices { get; set; }
       
        public List<SelectListItem> BodyStyleChoices { get; set; }
      
        public List<SelectListItem> Transmissions { get; set; }
        public List<SelectListItem> Type { get; set; }
        [DisplayName("Image")]
        public HttpPostedFileBase UploadedFile { get; set; }
        public CarDetailed AddCar { get; set; }


        public void SetModelChoice(List<CarModels> models)
        {
            List<SelectListItem> MC = new List<SelectListItem>();
            foreach (CarModels model in models)
            {

                MC.Add(new SelectListItem()
                {
                    Value = model.ModelID.ToString(),
                    Text = model.ModelName,
                    Selected=false
                });
            }
            ModelChoices = MC;
        }

        public void SetMakeChoice(List<Makes> makes)
        {
            List<SelectListItem> makec = new List<SelectListItem>();
            foreach (var make in makes)
            {
                makec.Add(new SelectListItem()
                {
                    Value = make.MakeID.ToString(),
                    Text = make.MakeName
                });
            }
            MakeChoices = makec;
        }


        public void SetSelectList(List<CarModels>models, List<Makes>makes, List<Colors> colors, List<Interiors>interiors, List<BodyStyles>bodies, List<Transmission> trans)
        {
            List<SelectListItem> MC = new List<SelectListItem>();
            foreach (CarModels model in models)
            {

                MC.Add(new SelectListItem()
                {
                    Value = model.ModelID.ToString(),
                    Text = model.ModelName,
                    Selected = false
                });
            }
            ModelChoices = MC;

            List<SelectListItem> makec = new List<SelectListItem>();
            foreach (var make in makes)
            {
                makec.Add(new SelectListItem()
                {
                    Value = make.MakeID.ToString(),
                    Text = make.MakeName,
                    Selected = false
                });
            }
            MakeChoices = makec;

            List<SelectListItem> col = new List<SelectListItem>();
            foreach (Colors color in colors)
            {
                col.Add(new SelectListItem()
                {
                    Value = color.ColorID.ToString(),
                    Text=color.ColorName,
                    Selected = false
                });
            }
            ColorChoices = col;

            List<SelectListItem> inside = new List<SelectListItem>();
            foreach (Interiors interior in interiors)
            {
                inside.Add(new SelectListItem()
                {
                    Value=interior.InteriorID.ToString(),
                    Text=interior.InteriorName,
                    Selected = false
                });
            }
            InteriorChoices = inside;

            List<SelectListItem> bstyle = new List<SelectListItem>();
            foreach (BodyStyles body in bodies)
            {
                bstyle.Add(new SelectListItem()
                {
                    Value =body.StyleID.ToString(),
                    Text= body.BodyStyle,
                    Selected = false
                });
            }
            BodyStyleChoices = bstyle;

            List<SelectListItem> tc = new List<SelectListItem>();

            foreach (Transmission t in trans)
            {
                tc.Add(new SelectListItem()
                {
                    Value = t.TransmissionID.ToString(),
                    Text= t.TransmissionName,
                    Selected = false
                });
            }
            Transmissions = tc;

            CarDetailed ac = new CarDetailed();
            ac.IsNew = true;
            CarDetailed bc = new CarDetailed();
            bc.IsNew = false;

            CarType forlist1 = new CarType();
            forlist1.IsNew = true;
            forlist1.Description = "New";
            CarType forlist2 = new CarType();
            forlist2.IsNew = false;
            forlist2.Description = "Used";
            List<CarType> ctypes = new List<CarType>() { forlist1, forlist2 };

            List<SelectListItem> isnew = new List<SelectListItem>();
            foreach(CarType ctype in ctypes)
            {
                isnew.Add(new SelectListItem()
                {
                    Value = ctype.IsNew.ToString(),
                    Text = ctype.Description,
                    Selected = false
                });
            }
            Type = isnew;
               
            
        }


        
    }
}