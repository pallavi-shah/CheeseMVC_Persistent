using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public int CheeseID { get; set; }

        public List<SelectListItem> Cheeses = new List<SelectListItem>();
        public int MenuID { get; set; }
        public Menu Menu { get; set; }
        public AddMenuItemViewModel()
        {

        }

        public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> cheeses)
        {
            foreach(Cheese ch in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = ch.ID.ToString(),
                    Text = ch.Name
                });
            }

            Menu = menu;
        }
    }
}
    