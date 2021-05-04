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

        // POST: Restrict/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                // update original review

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

    // GET: Restrict/Delete/5
    public async Task<IActionResult> DeleteAccount()
        {
            // retrieve user's PK from the Claims collection and assign it to userPK var

            int userPK = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);

            var aCustomer = await _context.Customers
               .FirstOrDefaultAsync(c => c.CustomerId == userPK);

            //if reviewpk is not valid

            if (aCustomer == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(aCustomer);
        }

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

    }
}
