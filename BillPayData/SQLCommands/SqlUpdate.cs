using System.Data.SqlClient;
using System.Data;

namespace BillPayData.SQLCommands
{
    public static class SqlUpdate
    {
        
        public static void UpdateBillStatus(string BillingID, string AccountNumber, string PayStatus, string ConnString)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("", con))
                {
                    cmd.Parameters.Add("@BillingID", SqlDbType.VarChar);
                    cmd.Parameters["@BillingID"].Value = BillingID;

                    cmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar);
                    cmd.Parameters["@AccountNumber"].Value = AccountNumber;

                    cmd.Parameters.Add("@PayStatus", SqlDbType.VarChar);
                    cmd.Parameters["@PayStatus"].Value = PayStatus;

                    cmd.CommandText = "UPDATE dbo.[BillInformation] " +
                        "SET PayStatus = @PayStatus " +
                        "WHERE dbo.[BillInformation].BillingID = @BillingID AND dbo.[BillInformation].AccountNumber = @AccountNumber";

                    cmd.ExecuteNonQuery();
                   
                }
            }
        }
    }
}
