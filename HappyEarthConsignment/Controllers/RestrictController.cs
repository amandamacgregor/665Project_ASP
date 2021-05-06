/*RestrictController with methods accessible only to authenticated site users such as placing an order and managing/deleting account info
Authors: Amanda MacGregor & Tara Schoenherr
References: Demo projects from LSV
Prepared: Spring 2021
Purpose: CIS 665 ASP Project
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HappyEarthConsignment.Model;
using HappyEarthConsignment.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HappyEarthConsignment.Controllers
{
    public class RestrictController : Controller
    {
        private readonly Team104dbContext _context;

        public RestrictController(Team104dbContext context)
        {
            _context = context;
        }

        // GET: Restrict/Edit/5
        //edit user account information
        [Authorize]
        public async Task<IActionResult> ManageAccount()
        {
            int customerID = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

            var customer = await _context.Customers.FindAsync(customerID);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // edit user account information
        // POST: Restrict/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageAccount(int id, [Bind("CustomerId,FirstName,LastName,Email,StreetAddress,Username,Password")] Customer aCustomer)
        {

            // retrieve user's PK from the Claims collection and assign it to userPK var

            int userPK = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

            // retrieve original account info and store in oCustomer var
            var oCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == userPK);

            if (oCustomer == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                // update original customer info

                oCustomer.FirstName = aCustomer.FirstName;
                oCustomer.LastName = aCustomer.LastName;
                oCustomer.Email = aCustomer.Email;
                oCustomer.StreetAddress = aCustomer.StreetAddress;
                oCustomer.Username = aCustomer.Username;
                if (!String.IsNullOrEmpty(aCustomer.Password)) { //checks if user entered a new password, if so updates password field
                    oCustomer.Password = aCustomer.Password;
                }

                try
                {
                    _context.Update(oCustomer);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    TempData["failure"] = $"Error - your account infromation was not updated";
                    return RedirectToAction(nameof(ManageAccount));
                }

                TempData["success"] = $"Your account information was successfully updated";
                return RedirectToAction(nameof(ManageAccount));
            }

            return View(aCustomer);
        }
    
    //delete user account
    // GET: Restrict/Delete/5
    public async Task<IActionResult> DeleteAccount()
        {
            // retrieve user's PK from the Claims collection and assign it to userPK var

            int userPK = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

            var aCustomer = await _context.Customers
               .FirstOrDefaultAsync(c => c.CustomerId == userPK);

            //if customer id is not valid

            if (aCustomer == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(aCustomer);
        }

        //delete user account and display confirmation page
        // POST: Restrict/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccountConfirmed()
        {
            int customerID = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

            // retrieve customer

            var aCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == customerID);

            //if CustomerID is not valid

            if (aCustomer == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                _context.Customers.Remove(aCustomer);
                await _context.SaveChangesAsync();
            }
            catch
            {
                {
                    TempData["failure"] = $"An Error occurred. Your account was not deleted";
                    return RedirectToAction(nameof(ManageAccount));
                }
            }

            TempData["success"] = $"Your account was successfully deleted";
            return RedirectToAction("LogOut", "Account");
        }
        
        //places user order in database 
        [Authorize]
        public IActionResult PlaceOrder()
        {
            Cart aCart = GetCart();

              //get customer ID of logged in user
                int userID = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

                //get sum of all products in cart
                decimal cartSum = aCart.ComputeOrderTotal();

                    // insert order
                    Order aOrder = new Order { CustomerId = userID, PlacedAt = DateTime.Now, Total = cartSum };

                _context.Add(aOrder);
                _context.SaveChanges();

                // get the PK of the newly inserted order

                int orderPK = aOrder.OrderId;

                //create viewData for order id to be referenced in the view
                ViewData["orderID"] = orderPK;

                // insert a orderdetail for each item in the cart

                foreach (CartItem aItem in aCart.CartItems())
                {
                    OrderLineItem aLine = new OrderLineItem { ProductId = aItem.Product.ProductId, OrderId = orderPK };
                    _context.Add(aLine);
                    
                    //make item unavailable in database
                    var product = aItem.Product;
                    product.Available = "N";

                    _context.Update(product);
                }

                _context.SaveChanges();

                // remove all items from cart

                aCart.ClearCart();

                SaveCart(aCart);

            return RedirectToAction("OrderConfirmation", new { orderID = orderPK});
        }

        //displays order confirmation page (page has option for user to cancel order)
        public IActionResult OrderConfirmation(int orderID)
        {
            return View(orderID);
        }

        //allows user to review shopping cart and confirm their order 
        [Authorize]
        public IActionResult ConfirmOrder()
        {
            Cart aCart = GetCart();

            return View(aCart);
        }

        //retrieves cart from session state
        Cart GetCart()
        {
            Cart aCart = HttpContext.Session.GetObject<Cart>("Cart") ?? new Cart();
            return aCart;
        }

        //saves cart to session state
        void SaveCart(Cart aCart)
        {
            // call the session extension method SetObject

            HttpContext.Session.SetObject("Cart", aCart);
        }

        //// GET: Restrict/Delete/5
        public async Task<IActionResult> DeleteOrder(int id)
        {
            // retrieve order based on order id
            var aOrder = await _context.Orders
               .FirstOrDefaultAsync(o => o.OrderId == id);

            //if for some reason order id is null redirect user to home page

            if (aOrder == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(aOrder);
        }

        //deletes user order
        // POST: Restrict/Delete/5
        [HttpPost, ActionName("DeleteOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrderConfirmed(int id)
        {
            //get the order
            var aOrder = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id);

            //get the orderlines that match the order id
            var orderLines = from o in _context.OrderLineItems select o;
            orderLines = orderLines.Where(o => o.OrderId.Equals(id));

            //if OrderID is not valid

            if (aOrder == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try 
            {
                //delete the associated order lines and mark products available in DB again
                foreach (OrderLineItem ol in orderLines) {
                    var productID = ol.ProductId;
                    var aProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productID);
                    aProduct.Available = "Y";

                    //remove order line and update product
                    _context.OrderLineItems.Remove(ol);
                    _context.Products.Update(aProduct);
                    
                }
             
                //delete the order itself
                _context.Orders.Remove(aOrder);
                await _context.SaveChangesAsync();
            }
            catch
            {
                {
                    TempData["failure"] = $"An Error occurred. Your order was not deleted";
                }
            }

            TempData["success"] = $"Your order was successfully deleted";
            return RedirectToAction(nameof(DeleteOrderConfirmation));
        }

        //confirms order was deleted
        public IActionResult DeleteOrderConfirmation() {
            return View();
        }
    }
}
