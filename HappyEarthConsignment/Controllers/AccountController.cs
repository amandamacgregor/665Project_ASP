

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//add the following namespaces

using Microsoft.EntityFrameworkCore;
using HappyEarthConsignment.Model;
using HappyEarthConsignment.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace HappyEarthConsignment.Controllers
{
    public class AccountController : Controller
    {
        private readonly Team104dbContext _context;

        public AccountController(Team104dbContext context)
        {
            _context = context;
        }

        // the returnURL captures the View the user was trying to reach before being redirected to the Login View

        public IActionResult Login(string returnURL)
        {
            // if returnURL is null or empty, it is set to "~/" (i.e., Home/Index)

            returnURL = String.IsNullOrEmpty(returnURL) ? "~/" : returnURL;

            // create a new instance of LoginInput and pass it to the Login View

            return View(new LoginInput { ReturnURL = returnURL });
        }


        // Post action (when user submits the Login form)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password,ReturnURL")] LoginInput loginInput)
        {
            if (ModelState.IsValid)
            {
                // check if login credentials are valid

                var aUser = await _context.Customers.FirstOrDefaultAsync(c => c.Username == loginInput.Username && c.Password == loginInput.Password);

                // if valid

                if (aUser != null)
                {
                    // From Microsoft documentation - "A claim is a statement about a subject by an issuer. Claims represent attributes of the subject that are useful in the context of authentication and authorization operations"

                    // Examples of claims would be data on a Driver's License card (i.e., name, date of birth)

                    var claims = new List<Claim>();

                    // the Type property can be used to store information about the claim

                    claims.Add(new Claim(ClaimTypes.Sid, aUser.CustomerId.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, aUser.FirstName));
                    claims.Add(new Claim(ClaimTypes.Name, aUser.LastName));
                    claims.Add(new Claim(ClaimTypes.Email, aUser.Email));

                    // From Microsoft documentation - "The ClaimsIdentity class is a concrete implementation of a claims-based identity; that is, an identity described by a collection of claims."

                    // a collection of claims can be used to create a ClaimsIndentity along with the authentication scheme (in this case, cookie-based authentication)

                    // Example of identity would be a Driver's License card

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // multiple identities can be stored in a ClaimsPrincipal

                    // Example, a user's multiple identities (driver's license, employee ID, passport) can make up a ClaimsPrincipal

                    var principal = new ClaimsPrincipal(identity);

                    // the SignInAsync method issues the authentication cookie to the user

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // return the user to the View they were originally trying to reach or Home/Index

                    return Redirect(loginInput?.ReturnURL ?? "~/");
                }

                // if credentials are not valid

                else
                {
                    ViewData["message"] = "Invalid credentials";
                }
            }

            // return user to Login View

            return View(loginInput);
        }

        // GET: Sign Up for an account
        public IActionResult SignUp()
        {
            return View();
        }

        // Post action (when user submits the new account form)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Username, Password, FirstName, LastName, Email, StreetAddress")] Customer customerInfo)
        {
            if (ModelState.IsValid)
            {
                // check for duplicate username

                var aUser = await _context.Customers.FirstOrDefaultAsync(c => c.Username == customerInfo.Username);

                // if no duplication

                if (aUser is null)
                {
                    // set default role to "user" and create new record in LoginInfo

                    _context.Add(customerInfo);
                    await _context.SaveChangesAsync();

                    TempData["success"] = "Account created";

                    // redirect to Login View

                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ViewData["message"] = "Username taken - please choose a different username";
                }
            }

            // return user to SignUp View

            return View(customerInfo);
        }

        // method to log user out and redirect to Home View
        public async Task<RedirectToActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
