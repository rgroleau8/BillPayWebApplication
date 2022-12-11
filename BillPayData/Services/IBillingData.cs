using BillPayData.Models;

namespace BillPayData.Services
{
    public interface IBillingData
    {
        BillInformation GetBill(string BillingID, string AccountNumber);

        bool UpdateBillStatus(string BillingID, string AccountNumber, string PayStatus);
    }
}
