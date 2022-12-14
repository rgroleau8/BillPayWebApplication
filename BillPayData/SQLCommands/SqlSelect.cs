using System.Data.SqlClient;
using System.Data;

namespace BillPayData.SQLCommands
{
    public static class SqlSelect
    {

        public static DataTable QueryAllBillInfo(string BillingID, string AccountNumber, string ConnString)
        {
           
            DataTable output = new DataTable();

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand("", con))
                {
                    cmd.Parameters.Add("@BillingID", SqlDbType.VarChar);
                    cmd.Parameters["@BillingID"].Value = BillingID;

                    cmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar);
                    cmd.Parameters["@AccountNumber"].Value = AccountNumber;

                    cmd.CommandText = "SELECT dbo.[BillInformation].BillingID AS BillingID, dbo.[BillInformation].AccountNumber AS AccountNumber, dbo.[BillInformation].PayStatus As PayStatus, dbo.[BillInformation].Amount AS Amount, dbo.[VisitInformation].VisitID AS VisitID, dbo.[VisitInformation].DateOfService AS DOS, dbo.[Location].LocationName AS LocationName, dbo.[Location].LocationAddress AS LocationAddress, dbo.[Provider].ProviderName AS ProviderName, dbo.[Speciality].SpecialityName AS SpecialityName, dbo.[TypeOfVisit].VisitTypeName AS VisitTypeName " +
                        "FROM dbo.[BillInformation] " +
                        "INNER JOIN dbo.[VisitInformation] ON dbo.[VisitInformation].VisitID = dbo.[BillInformation].VisitID " +
                        "INNER JOIN dbo.[Location] ON dbo.[Location].LocationID = dbo.[VisitInformation].LocationID " +
                        "INNER JOIN dbo.[Provider] ON dbo.[Provider].ProviderID = dbo.[VisitInformation].ProviderID " +
                        "INNER JOIN dbo.[TypeOfVisit] ON dbo.[TypeOfVisit].VisitTypeID = dbo.[VisitInformation].VisitTypeID " +
                        "INNER JOIN dbo.[Speciality] ON dbo.[Speciality].SpecialityID = dbo.[Provider].SpecialityID " +
                        "WHERE BillingID = @BillingID AND AccountNumber = @AccountNumber";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(output);
                    }
                }
            }

            return output;
        }

        public static bool ValidateBillExists(string BillingID, string AccountNumber, string ConnString)
        {
            bool output = false;

            using (SqlConnection con = new SqlConnection(ConnString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("", con))
                {
                    cmd.Parameters.Add("@BillingID", SqlDbType.VarChar);
                    cmd.Parameters["@BillingID"].Value = BillingID;

                    cmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar);
                    cmd.Parameters["@AccountNumber"].Value = AccountNumber;

                    cmd.CommandText = "SELECT dbo.[BillInformation].BillingID AS BillingID, dbo.[BillInformation].AccountNumber AS AccountNumber " +
                        "FROM dbo.[BillInformation] " +
                        "WHERE BillingID = @BillingID AND AccountNumber = @AccountNumber";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            output = true;
                    }
                }
            }

            return output;
        }
    }
}
