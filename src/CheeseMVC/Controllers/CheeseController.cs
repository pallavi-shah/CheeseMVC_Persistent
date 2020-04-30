using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext context;
        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Cheese> cheeses = context.Cheeses.Include(c=>c.Category).ToList();
            return View(cheeses);
        }


        // GET - ADD CHEESE METHOD
        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList()); ;
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            CheeseCategory cheeseCategory = context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);

            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing cheeses
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Category=cheeseCategory
                };
                context.Cheeses.Add(newCheese);
                context.SaveChanges();
                return Redirect("/Cheese");
            }
            AddCheeseViewModel vm = new AddCheeseViewModel(context.Categories.ToList());
            vm.Name = addCheeseViewModel.Name;
            vm.Description = addCheeseViewModel.Description;
            vm.CategoryID = addCheeseViewModel.CategoryID;
            return View(vm);
        }   

        // GET - REMOVE CHEESE METHOD
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
            return View();
        }

        // POST-  REMOVE CHEESE METHOD
        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);
            }

            context.SaveChanges();
            return Redirect("/");
        }

        // GET - EDIT CHEESE METHOD
        public IActionResult Edit(int cheeseId)
        {
            Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
            //return View(theCheese);
            EditCheeseViewModel editCheeseViewModel = new EditCheeseViewModel(context.Categories.ToList())
            {
                Name = theCheese.Name,
                Description = theCheese.Description,
                CategoryID = theCheese.CategoryID
            };
            ViewBag.id = cheeseId;
            ViewBag.name = theCheese.Name;
            return View(editCheeseViewModel);
        }

        // POST- EDIT CHEESE METHOD
        [HttpPost]
        public IActionResult Edit(int cheeseId, EditCheeseViewModel editCheeseViewModel)
        {
            Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);

            if (ModelState.IsValid) 
            {
                theCheese.Name = editCheeseViewModel.Name;
                theCheese.Description = editCheeseViewModel.Description;
                theCheese.CategoryID = editCheeseViewModel.CategoryID;            
                context.SaveChanges();
                return Redirect("/Cheese/Index");
            }

            EditCheeseViewModel vm = new EditCheeseViewModel(context.Categories.ToList());
            ViewBag.id = cheeseId;
            ViewBag.name = theCheese.Name;
            vm.Name = editCheeseViewModel.Name;
            vm.Description = editCheeseViewModel.Description;
            vm.CategoryID = editCheeseViewModel.CategoryID;
            return View(vm);
            //return View(theCheese);

        }
    }
}
