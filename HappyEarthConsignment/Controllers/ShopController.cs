/*ShopController with methods to search products and manage shopping cart
Authors: Amanda MacGregor & Tara Schoenherr
References: Demo projects from LSV
Prepared: Spring 2021
Purpose: CIS 665 ASP Project
 */

using HappyEarthConsignment.Model;
using HappyEarthConsignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyEarthConsignment.Controllers
{
    public class ShopController : Controller
    {

        private readonly Team104dbContext _context;
        public ShopController(Team104dbContext context)
        {
            _context = context;
        }

        //Prepare a SelectList for the Gender Options
        public async Task<IActionResult> Search(string searchGender, string searchCondition, string searchSize, int searchCategory, decimal? priceMin, decimal? priceMax, DateTime? startDate, DateTime? endDate)
        {
            List<SelectListItem> genderList = GenderList(searchGender);
            List<SelectListItem> conditionList = ConditionList(searchCondition);
            List<SelectListItem> sizeList = SizeList(searchSize);
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

            //start by only including products marked available in the DB
            var products = from p in _context.Products select p;
            products = products.Where(p => p.Available.Contains("Y"));

            //then filter by user inputs
            if (String.Equals(searchGender, "All"))
            {
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
        private List<SelectListItem> GenderList(String searchGender)
        {
            List<SelectListItem> genderList = new List<SelectListItem>();

            //for each value check if user selected value has been passed through that matches - if so set as selected value
            if ((!String.IsNullOrEmpty(searchGender)) && (String.Equals(searchGender, "All"))){
                genderList.Add(new SelectListItem { Text = "All Genders", Value = "All", Selected = true });
            }
            else
            {
                genderList.Add(new SelectListItem { Text = "All Genders", Value = "All" });
            }

            if ((!String.IsNullOrEmpty(searchGender)) && (String.Equals(searchGender, "F")))
            {
                genderList.Add(new SelectListItem { Text = "Female", Value = "F", Selected = true });
            }
            else {
                genderList.Add(new SelectListItem { Text = "Female", Value = "F" });
            }

            if ((!String.IsNullOrEmpty(searchGender)) && (String.Equals(searchGender, "N")))
            {
                genderList.Add(new SelectListItem { Text = "Gender Non-Binary", Value = "N", Selected = true });
            }
            else 
            {
                genderList.Add(new SelectListItem { Text = "Gender Non-Binary", Value = "N" });
            }

            if ((!String.IsNullOrEmpty(searchGender)) && (String.Equals(searchGender, "M")))
            {
                genderList.Add(new SelectListItem { Text = "Male", Value = "M", Selected = true });
            }
            else 
            {
                genderList.Add(new SelectListItem { Text = "Male", Value = "M" });
            }

            return genderList;
        }

        //Prepare a SelectList for the Condition Options
        private List<SelectListItem> ConditionList(string searchCondition)
        {

            List<SelectListItem> conditionList = new List<SelectListItem>();

            conditionList.Add(new SelectListItem { Text = "Select a Condition", Value = null });

            //for each value check if user selected value has been passed through that matches - if so set as selected value
            if ((!String.IsNullOrEmpty(searchCondition)) && (String.Equals(searchCondition, "Used")))
            {
                conditionList.Add(new SelectListItem { Text = "Used", Value = "Used", Selected = true });
            }
            else 
            {
                conditionList.Add(new SelectListItem { Text = "Used", Value = "Used" });
            }

            if ((!String.IsNullOrEmpty(searchCondition)) && (String.Equals(searchCondition, "Like New")))
            {
                conditionList.Add(new SelectListItem { Text = "Like New", Value = "Like New", Selected = true });
            }
            else
            {
                conditionList.Add(new SelectListItem { Text = "Like New", Value = "Like New" });
            }

            if ((!String.IsNullOrEmpty(searchCondition)) && (String.Equals(searchCondition, "New")))
            {
                conditionList.Add(new SelectListItem { Text = "New", Value = "New", Selected = true });
            }
            else
            {
                conditionList.Add(new SelectListItem { Text = "New", Value = "New" });
            }

            return conditionList;
        }

        //Prepare a SelectList for the Size Options
        private List<SelectListItem> SizeList(string searchSize)
        {

            List<SelectListItem> sizeList = new List<SelectListItem>();

            sizeList.Add(new SelectListItem { Text = "Select a Size", Value = null });

            //for each value check if user selected value has been passed through that matches - if so set as selected value

            if ((!String.IsNullOrEmpty(searchSize)) && (String.Equals(searchSize, "Small")))
            {
                sizeList.Add(new SelectListItem { Text = "Small", Value = "Small", Selected = true });
            }
            else 
            {
                sizeList.Add(new SelectListItem { Text = "Small", Value = "Small" });
            }

            if ((!String.IsNullOrEmpty(searchSize)) && (String.Equals(searchSize, "M")))
            {
                sizeList.Add(new SelectListItem { Text = "Medium", Value = "M", Selected = true });
            }
            else
            {
                sizeList.Add(new SelectListItem { Text = "Medium", Value = "M" });
            }

            if ((!String.IsNullOrEmpty(searchSize)) && (String.Equals(searchSize, "Large")))
            {
                sizeList.Add(new SelectListItem { Text = "Large", Value = "Large", Selected = true });
            }
            else
            {
                sizeList.Add(new SelectListItem { Text = "Large", Value = "Large" });
            }

            if ((!String.IsNullOrEmpty(searchSize)) && (String.Equals(searchSize, "1x")))
            {
                sizeList.Add(new SelectListItem { Text = "Extra-Large", Value = "1x", Selected = true });
            }
            else
            {
                sizeList.Add(new SelectListItem { Text = "Extra-Large", Value = "1x" });
            }

            if ((!String.IsNullOrEmpty(searchSize)) && (String.Equals(searchSize, "1x")))
            {
                sizeList.Add(new SelectListItem { Text = "2XL", Value = "1x", Selected = true });
            }
            else
            {
                sizeList.Add(new SelectListItem { Text = "2XL", Value = "2x" });
            }

            if ((!String.IsNullOrEmpty(searchSize)) && (String.Equals(searchSize, "1x")))
            {
                sizeList.Add(new SelectListItem { Text = "2XL", Value = "1x", Selected = true });
            }
            else
            {
                sizeList.Add(new SelectListItem { Text = "3XL", Value = "3x" });
            }

            return sizeList;
        }

        // prepare output to display items in cart object 
        public IActionResult MyCart()
        {
            Cart aCart = GetCart();

            //if (aCart.CartItems().Any())
            //{
            return View(aCart);
            //}

            //// if the cart is empty

            //return RedirectToAction(nameof(Search));
        }

        //method to retrieve cart object from session state 
        private Cart GetCart()
        {
            // call the session extension method GetObject
            // if a cart object doesn't exist, create a new cart object

            Cart aCart = HttpContext.Session.GetObject<Cart>("Cart") ?? new Cart();
            return aCart;
        }

        //method to save cart object to session state 
        private void SaveCart(Cart aCart)
        {
            // call the session extension method SetObject

            HttpContext.Session.SetObject("Cart", aCart);
        }

        // add a product to shopping cart
        public IActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Search));
            }

            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return RedirectToAction(nameof(Search));
            }

            // call to method to retrieve cart object from session state

            Cart aCart = GetCart();

            aCart.AddItem(product);

            // call to method to save cart object to session state

            SaveCart(aCart);

            return RedirectToAction(nameof(MyCart));
        }

        // remove product from shopping cart
        public IActionResult RemoveFromCart(int? productId)
        {
            if (productId == null)
            {
                return RedirectToAction(nameof(Search));
            }

            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return RedirectToAction(nameof(Search));
            }

            Cart aCart = GetCart();

            aCart.RemoveItem(product);

            SaveCart(aCart);

            return RedirectToAction(nameof(MyCart));
        }

        //details action method to show product details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Search));
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return RedirectToAction(nameof(Search));
            }

            return View(product);
        }
    }


}
