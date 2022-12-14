using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayData.Models.Settings
{
    public class MySettingsModel
    {
        public string BillInfoDB { get; set; }
        public string StripeApiSecret { get; set; }
        public string StripeWebHookSecret { get; set; }
    }
}
