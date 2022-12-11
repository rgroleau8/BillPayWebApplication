using System.Data.SqlClient;
using System.Data;

namespace BillPayData.SQLCommands
{
    public static class SqlUpdate
    {
        static string conn_string = SQLConnectionStrings.BillInformationConnString;

        public static void UpdateBillStatus(string BillingID, string AccountNumber, string PayStatus)
        {
            using (SqlConnection con = new SqlConnection(conn_string))
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
