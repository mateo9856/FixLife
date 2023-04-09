﻿using FixLife.WebApiDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiDomain.Plan
{
    public class LearnTime : BaseBusinessEntity
    {
        public bool IsYearly { get; set; }
        public bool IsWeekly { get; set; }
        public TimeSpan TimeInterval { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}
