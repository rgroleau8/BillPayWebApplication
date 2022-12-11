﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayData.Models
{
    public class Provider
    {
        public Provider()
        {
            this.Speciality = new Speciality();
        }
        public string ProviderID { get; set; }

        public string ProviderName { get; set; }

        public Speciality Speciality { get; set; }
    }
}
