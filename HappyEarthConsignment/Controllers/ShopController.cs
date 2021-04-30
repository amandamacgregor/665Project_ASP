using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Add the following namespaces

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using HappyEarthConsignment.Model;

namespace HappyEarthConsignment.Controllers
{
    public class ShopController : Controller
    {

        private readonly Team104dbContext _context;
        public ShopController(Team104dbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Search(string searchGender, string searchCondition, string searchSize, int searchCategory, decimal? priceMin, decimal? priceMax, DateTime? startDate, DateTime? endDate)
        {
            List<SelectListItem> genderList = GenderList();
            List<SelectListItem> conditionList = ConditionList();
            List<SelectListItem> sizeList = SizeList();
            ViewData["GenderList"] = genderList;
            ViewData["ConditionList"] = conditionList;
            ViewData["SizeList"] = sizeList;
            ViewData["GenderFilter"] = searchGender;
            ViewData["ConditionFilter"] = searchCondition;
            ViewData["SizeFilter"] = searchSize;

            List<SelectListItem> categoriesList = new SelectList(_context.ProductCategories.OrderBy(c => c.Name), "ProductCategoryId", "Name", searchCategory).ToList();
            categoriesList.Insert(0, (new SelectListItem { Text = "Select a Category", Value = "0" }));

            ViewData["CategoryFilter"] = categoriesList;
            ViewData["PriceMinFilter"] = priceMin;
            ViewData["PriceMaxFilter"] = priceMax;
            ViewData["StartDateFilter"] = startDate;
            ViewData["EndDateFilter"] = endDate;

            var products = from p in _context.Products select p;

            if (String.Equals(searchGender, "All")) {
                products = products.Where(p => p.Gender.Contains("M") || p.Gender.Contains("F") || p.Gender.Contains("N"));
            }

            else if (!String.IsNullOrEmpty(searchGender))
            {
                products = products.Where(p => p.Gender.Contains(searchGender));
            }

            if ((!String.IsNullOrEmpty(searchCondition)) && !String.Equals(searchCondition, "Select a Condition"))
            {
                products = products.Where(p => p.Condition.Equals(searchCondition));
            }

            if ((!String.IsNullOrEmpty(searchCondition)) && (!String.Equals(searchSize, "Select a Size")))
            {
                products = products.Where(p => p.Size.Equals(searchSize));
            }

            if (searchCategory > 0)
            {
                products = products.Where(p => p.CategoryId == searchCategory);
            }

            if (priceMin != null)
            {
                products = products.Where(p => p.Price >= priceMin);
            }
            if (priceMax != null)
            {
                products = products.Where(p => p.Price <= priceMax);
            }
            if ((startDate != null) && (endDate != null))
            {
                products = products.Where(p => p.Created >= startDate && p.Created <= endDate);
            }

            return View(await products.Include(p => p.Category).OrderBy(p => p.Name).ToListAsync());
        }

        //Prepare a SelectList for the Gender Options
        private List<SelectListItem> GenderList()
        { 
            List<SelectListItem> genderList = new List<SelectListItem>();

                genderList.Add(new SelectListItem { Text = "All Genders", Value = "All"});

                genderList.Add(new SelectListItem { Text = "Female", Value = "F" });

                genderList.Add(new SelectListItem { Text = "Gender Non-Binary", Value = "N" });

                genderList.Add(new SelectListItem { Text = "Male", Value = "M" });

                
                return genderList;
        }

        //Prepare a SelectList for the Condition Options
        private List<SelectListItem> ConditionList()
        {

            List<SelectListItem> conditionList = new List<SelectListItem>();

            conditionList.Add(new SelectListItem { Text = "Select a Condition", Value = null });

            conditionList.Add(new SelectListItem { Text = "Used", Value = "Used" });

            conditionList.Add(new SelectListItem { Text = "Like New", Value = "Like New" });

            conditionList.Add(new SelectListItem { Text = "New", Value = "New" });

            return conditionList;
        }

        //Prepare a SelectList for the Condition Options
        private List<SelectListItem> SizeList()
        {

            List<SelectListItem> sizeList = new List<SelectListItem>();

            sizeList.Add(new SelectListItem { Text = "Select a Size", Value = null });

            sizeList.Add(new SelectListItem { Text = "Small", Value = "Small" });

            sizeList.Add(new SelectListItem { Text = "Medium", Value = "M" });

            sizeList.Add(new SelectListItem { Text = "Large", Value = "Large" });

            sizeList.Add(new SelectListItem { Text = "Extra-Large", Value = "1x" });

            sizeList.Add(new SelectListItem { Text = "2XL", Value = "2x" });

            sizeList.Add(new SelectListItem { Text = "3XL", Value = "3x" });

            return sizeList;
        }
    }
}
