using Blood_Donation.Models;
using Blood_Donation.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Drawing;


namespace Blood_Donation.Controllers
{
    public class BloodDonorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly DatabaseService _dbService;

        public BloodDonorController(ApplicationDbContext context, IConfiguration configuration, DatabaseService dbService)

        {
            _context = context;
            _configuration = configuration;
            _dbService = dbService;
        }

        public IActionResult CreateDonor()
        {
            try
            {
                GetBloodGroup();
                bool isConnected = _dbService.ConnectionMethod(_configuration);
                ViewBag.ConnectionStatus = isConnected ? "Connected to Database!" : "Failed to Connect.";
                //In case ViewBag is not set due to some issue, set a default message
                if (ViewBag.ConnectionStatus == null)
                {
                    ViewBag.ConnectionStatus = "Connection status is unknown.";
                }
                return View();
            }
            catch (Exception ex)
            {
                // Set a user-friendly error message
                ViewBag.Message = "An error occurred while processing your request. Please try again later.";

                // Optionally, log the error details
                TempData["ErrorMessage"] = ex.Message;

                // Return the view (possibly with error details)
                return View();

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDonor(RegisterDonor donor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Retrieve the last inserted ID
                        //var lastInsertedId = _context.DonorList
                        //    .FromSqlRaw("SELECT IDENT_CURRENT('DonorList') AS LastInsertedId")
                        //    .AsEnumerable()
                        //    .FirstOrDefault()?.LastInsertedId;
                        // Assuming you are using Entity Framework for database operations
                        _context.DonorList.Add(donor);
                        _context.SaveChanges();
                        // Redirect to a success page or display a success message
                        return RedirectToAction("GetDonor");  // Redirect to a list page or success page
                    }
                    catch (Exception ex)
                    {
                        // Handle error (log it and return an error view)
                        ViewBag.ErrorMessage = "There was an error saving the donor. Please try again.";
                        return View(donor); 
                    }
                }
                GetBloodGroup();
                // In case the model is invalid, return to the form with validation errors
                return View(donor);
            }
            catch (Exception ex)
            {
                // Set a user-friendly error message
                ViewBag.Message = "An error occurred while processing your request. Please try again later.";

                // Optionally, log the error details
                TempData["ErrorMessage"] = ex.Message;

                // Return the view (possibly with error details)
                return View();
            }
        }

        public IActionResult GetDonor(string searchTerm, string citySearchTerm)
        {
            try
            {
                // Check if the user has entered a name to search for
                if (!string.IsNullOrEmpty(searchTerm) && string.IsNullOrEmpty(citySearchTerm))
                {
                    // Search by name
                    var results = _context.DonorList.Where(d => d.BloodGroup.Contains(searchTerm)).ToList();
                    return View(results); // Return results for name search
                }

                // Check if the user has entered a city to search for
                if (!string.IsNullOrEmpty(citySearchTerm) && string.IsNullOrEmpty(searchTerm))
                {
                    // Search by city
                    var results = _context.DonorList.Where(d => d.City.Contains(citySearchTerm)).ToList();
                    return View(results); // Return results for city search
                }
                if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrEmpty(citySearchTerm))
                {
                    var results = _context.DonorList
           .Where(d => d.City.Contains(citySearchTerm) && d.BloodGroup.Contains(searchTerm)).ToList();
                    return View(results); // Return results for both city and blood group search
                }
                if (string.IsNullOrEmpty(searchTerm) && string.IsNullOrEmpty(citySearchTerm))
                {
                    // Search by city
                    var results = _context.DonorList.ToList();
                    return View(results); // Return results for All search
                }
                // If neither search term is provided, return the default view or a message
                ViewBag.Message = "Please enter a name or city to search.";
                return View();  // Optionally, you can return a default view or an empty search result.

            }
            catch (Exception ex)
            {
                // Set a user-friendly error message
                ViewBag.Message = "An error occurred while processing your request. Please try again later.";

                // Optionally, log the error details
                TempData["ErrorMessage"] = ex.Message;

                // Return the view (possibly with error details)
                return View();
            }
        }

        public void GetBloodGroup()
        {
            try
            {
                var lstBloodGroup = new List<SelectListItem>
            {
                new SelectListItem { Text = "A+", Value = "A+" },
                 new SelectListItem { Text = "A-", Value = "A-" },
        new SelectListItem { Text = "B+", Value = "B+" },
        new SelectListItem { Text = "B-", Value = "B-" },
        new SelectListItem { Text = "O+", Value = "O+" },
        new SelectListItem { Text = "O-", Value = "O-" },
        new SelectListItem { Text = "AB+", Value = "AB+" },
        new SelectListItem { Text = "AB-", Value = "AB-" }
            };
                // Passing the blood groups to the view via ViewData
                ViewData["BloodGroups"] = new SelectList(lstBloodGroup, "Value", "Text");
            }
            catch (Exception ex)
            {
                // Set a user-friendly error message
                ViewBag.Message = "An error occurred while processing your request. Please try again later.";

                // Optionally, log the error details
                TempData["ErrorMessage"] = ex.Message;

                // Return the view (possibly with error details)
                //return View();
            }
        }

    }
}
