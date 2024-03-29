﻿using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using BillPayData.Models;
using BillPayData.Services;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using BillPayData.Models.Settings;

namespace BillPayWebApplication.Controllers
{
    public class PaymentsController : Controller
    {
        private IBillingData db;
        private IOptions<MySettingsModel> appSettings;

        

        public PaymentsController(IBillingData db, IOptions<MySettingsModel> app)
        {
            this.appSettings = app;
            StripeConfiguration.ApiKey = this.appSettings.Value.StripeApiSecret;
            
            this.db = db;
        }

        [HttpPost]
        public IActionResult Create(string BillingID, string AccountNumber)
        {

            var domain = "https://rdgbillingapp.azurewebsites.net/";

            BillInformation billInfo = db.GetBill(BillingID, AccountNumber);

            if (billInfo == null)
            {
                return PartialView("~/Views/Shared/_BillingNotFound.cshtml");
            }

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions

                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (int)(billInfo.Amount * 100), //translates bill amount to pennies for stripe
                            Currency = "usd",

                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Medical Bill",
                            },
                        },
                        Quantity = 1,
                    },
                },

                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    //This metadata is sent to the checkout session and hooked back into the application in StripeWebHookController after the bill succeeds or fails
                    Metadata = new Dictionary<string, string>
                    {
                        { "BillingID", billInfo.BillingID },
                        { "AccountNumber", billInfo.AccountNumber }
                    }
                },

                Mode = "payment",

                SuccessUrl = domain + "/Success",
                CancelUrl = domain + "/Failed",
        };

        var service = new SessionService();

        Session session = service.Create(options);

        Response.Headers.Add("Location", session.Url);

        return new StatusCodeResult(303); //retuns the stripe checkout session created


        }
    }
}
