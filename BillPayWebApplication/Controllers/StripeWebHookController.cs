using Microsoft.AspNetCore.Mvc;
using Stripe;
using BillPayData.Services;
using Microsoft.Extensions.Options;
using BillPayData.Models;


namespace BillPayWebApplication.Controllers
{
    public class StripeWebHookController : Controller
    {
        private IBillingData db;
        private IOptions<MySettingsModel> appSettings;
        public StripeWebHookController(IBillingData db, IOptions<MySettingsModel> app)
        {
            this.appSettings = app;
            this.db = db;
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
                        //log
                    }
                }
                else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
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
