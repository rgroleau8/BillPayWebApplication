using Microsoft.AspNetCore.Mvc;
using Stripe;
using BillPayData.Services;
using Microsoft.Extensions.Options;
using BillPayData.Models.Settings;

namespace BillPayWebApplication.Controllers
{
    public class StripeWebHookController : Controller
    {
        private IBillingData db;
        private IOptions<MySettingsModel> appSettings;
        private ILog logger;

        public StripeWebHookController(IBillingData db, IOptions<MySettingsModel> app, ILog logger)
        {
            this.appSettings = app;
            this.db = db;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            string stripeWebHookSecret = appSettings.Value.StripeWebHookSecret;

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], stripeWebHookSecret,300, false); 

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                    string BillingID = paymentIntent.Metadata["BillingID"];
                    string AccountNumber = paymentIntent.Metadata["AccountNumber"];

                    if (db.UpdateBillStatus(BillingID, AccountNumber, "Paid") == false)
                    {
                        logger.Log($"Failed to update bill in database with BillingID: {BillingID} and Account Number: {AccountNumber}");
                    }
                }
                else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                }
                else
                {
                    logger.Log($"Unhandled event type: {stripeEvent.Type}");
                }

                return Ok();

            }
            catch (StripeException e)
            {
                return BadRequest();
            }

        }
    }
}
