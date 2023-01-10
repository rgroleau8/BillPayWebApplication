using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayData.Models.Enums
{
    public class Enums
    {
        public enum Providers
        {
            Provider1,
            Provider2,
            Provider3
        }

        public enum PayStatus
        {
            Unpaid,
            Waived,
            Pending,
            Paid
        }
    }


}
