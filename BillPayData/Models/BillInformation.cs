using System.ComponentModel.DataAnnotations;



namespace BillPayData.Models
{
    public class BillInformation
    {

        public BillInformation()
        {
            this.VisitInformation = new VisitInfo();
        }

        [Required]
        [Display(Name = "Billing Number")]
        public string BillingID { get; set; }


        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        public string Provider { get; set; }

        [Display(Name = "Status of Bill")]
        public string PayStatus { get; set; }

        [Display(Name = "Bill Amount")]
        public decimal Amount { get; set; }


        public int VisitID { get; set; }

        public VisitInfo VisitInformation { get; set; }



    }
}
