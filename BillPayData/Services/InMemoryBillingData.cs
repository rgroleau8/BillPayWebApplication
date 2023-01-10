using BillPayData.Models;
using BillPayData.Models.Enums;

namespace BillPayData.Services
{
    public class InMemoryBillingData : IBillingData
    {

        List<BillInformation> listOfBills;
        List<VisitInfo> listOfVisits;
        List<TypeOfVisit> listOfTypesOfVisit;
        List<Location> listOfLocations;

        public InMemoryBillingData()
        {
            listOfBills = new List<BillInformation>()
            {
                new BillInformation { BillingID = "1", AccountNumber = "1", Amount = 100.21m, PayStatus = Enums.PayStatus.Unpaid.ToString(), VisitID = 1},
                new BillInformation { BillingID = "2", AccountNumber = "2", Amount = 254.45m, PayStatus = Enums.PayStatus.Unpaid.ToString(), VisitID = 2 }
            };

            listOfVisits = new List<VisitInfo>()
            {
                new VisitInfo{ Id = 1, LocationID = 1, VisitTypeID = 1 , DateOfService = "10/12/2022"},
                new VisitInfo { Id = 2, LocationID = 2, VisitTypeID = 2, DateOfService = "10/9/2022"} 
            };


            listOfTypesOfVisit = new List<TypeOfVisit>()
            {
                new TypeOfVisit { Id = 1, Type = "Office Visit"},
                new TypeOfVisit { Id = 2, Type = "Exam"}
            };

            listOfLocations = new List<Location>()
            {
                new Location { LocationId = 1, LocationName = "Hospital1"},
                new Location { LocationId = 2, LocationName = "Hospital2"}
            };
        }

        public BillInformation GetBill(string BillingID, string AccountNumber)
        {
            BillInformation billInformation = listOfBills.Find(b => (b.BillingID == BillingID) && (b.AccountNumber == AccountNumber));

            if (billInformation == null)
            {
                return billInformation;
            }

            VisitInfo visitInfo = new VisitInfo();
                
            visitInfo = listOfVisits.Find(v => (v.Id == billInformation.VisitID));

            Location location = new Location();

            location = listOfLocations.Find(l => (l.LocationId == visitInfo.LocationID));

            TypeOfVisit typeOfvisit = new TypeOfVisit();

            typeOfvisit = listOfTypesOfVisit.Find(t => (t.Id == visitInfo.VisitTypeID));

            visitInfo.VisitType = typeOfvisit;
            visitInfo.Location = location;

            billInformation.VisitInformation = visitInfo;

            return billInformation;

        }

        public bool UpdateBillStatus(string BillingID, string AccountNumber, string PayStatus)
        {
            throw new NotImplementedException();
        }
    }
}
