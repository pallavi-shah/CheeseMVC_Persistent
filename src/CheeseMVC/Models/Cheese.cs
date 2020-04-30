using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheeseMVC.Models
{
    public class Cheese 
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
       
        public int ID { get; set; }
        public CheeseCategory Category { get; set; }

        public int CategoryID { get; set; }

        public IList<CheeseMenu> CheeseMenus { get; set; }
    }
}
