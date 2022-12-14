using BillPayData.Models;
using Microsoft.AspNetCore.Mvc;
using BillPayData.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using BillPayData.Models.Settings;

namespace BillPayWebApplication.Controllers
{
    public class PatientPortalController : Controller
    {

        private IBillingData db;
        private readonly IOptions<MySettingsModel> appSettings;

        public PatientPortalController(IBillingData db, IOptions<MySettingsModel> app)
        {
            this.db = db;
            this.appSettings = app;
            
            
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();

        }

        [Authorize]
        [HttpGet]
        public IActionResult Alerts()
        {
            return PartialView("~/Views/Shared/_Alerts.cshtml");
        }

        [Authorize]
        [HttpGet]
        public IActionResult BillingSearch()
        {
           
            return PartialView("~/Views/Shared/_BillingLookupSearch.cshtml");
        }


        [HttpPost]
        public IActionResult BillingDetails(string BillingID, string AccountNumber)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Shared/_BillingNotFound.cshtml"); // bill not found
            }


            BillInformation billInfo = db.GetBill(BillingID,AccountNumber);

            if (billInfo == null)
            {
                return PartialView("~/Views/Shared/_BillingNotFound.cshtml");//Bill not found
            }

            return PartialView("~/Views/Shared/_BillingDetails.cshtml", billInfo);

        }


     
    }
}