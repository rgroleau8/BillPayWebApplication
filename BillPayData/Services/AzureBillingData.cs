using BillPayData.Models;
using System.Data.SqlClient;
using System.Data;
using BillPayData.SQLCommands;
using Microsoft.Extensions.Options;
using BillPayData.Models.Settings;

namespace BillPayData.Services
{
    public class AzureBillingData : IBillingData
    {

        private IOptions<MySettingsModel> appSettings;

        public BillInformation GetBill(string BillingID, string AccountNumber)
        {

            DataTable queryResults = SqlSelect.QueryAllBillInfo(BillingID, AccountNumber, appSettings.Value.BillInfoDB);

            BillInformation billInfo = new BillInformation();

            if (queryResults.Rows.Count < 1)
            {
                //will return a null BillInformation object
                return billInfo; 
            }

            billInfo.BillingID = queryResults.Rows[0]["BillingID"].ToString();
            billInfo.AccountNumber = queryResults.Rows[0]["AccountNumber"].ToString();
            billInfo.PayStatus = queryResults.Rows[0]["PayStatus"].ToString();
            billInfo.Amount = Convert.ToDecimal(queryResults.Rows[0]["Amount"]);
            billInfo.VisitInformation.DateOfService = queryResults.Rows[0]["DOS"].ToString();
            billInfo.VisitInformation.Location.LocationName = queryResults.Rows[0]["LocationName"].ToString();
            billInfo.VisitInformation.Location.LocationAddress = queryResults.Rows[0]["LocationAddress"].ToString();
            billInfo.VisitInformation.Provider.ProviderName = queryResults.Rows[0]["ProviderName"].ToString();
            billInfo.VisitInformation.Provider.Speciality.SpecialityName = queryResults.Rows[0]["SpecialityName"].ToString();
            billInfo.VisitInformation.VisitType.Type = queryResults.Rows[0]["VisitTypeName"].ToString();
            

            return billInfo;
        }
        
        public bool UpdateBillStatus(string BillingID, string AccountNumber, string PayStatus)
        {

            if (SqlSelect.ValidateBillExists(BillingID, AccountNumber, appSettings.Value.BillInfoDB) == false)
                return false;

            SqlUpdate.UpdateBillStatus(BillingID, AccountNumber, PayStatus, appSettings.Value.BillInfoDB);

            return true;
        }
    }
}
