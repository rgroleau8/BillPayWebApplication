﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayData.Services
{
    public class Logger : ILog
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
