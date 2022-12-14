using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayData.Models.Bill
{
    public class VisitInfo
    {

        public VisitInfo()
        {
            Location = new Location();
            VisitType = new TypeOfVisit();
            Provider = new Provider();
        }
        public int Id { get; set; }

        public int LocationID { get; set; }

        public int VisitTypeID { get; set; }

        public int ProviderID { get; set; }

        public string DateOfService { get; set; }

        public Location Location { get; set; }

        public TypeOfVisit VisitType { get; set; }

        public Provider Provider { get; set; }

    }
}
