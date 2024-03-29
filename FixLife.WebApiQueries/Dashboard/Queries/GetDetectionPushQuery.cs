﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDetectionPushQuery : IRequest<GetDetectionPushResponse>
    {
        public string UserId { get; set; }
    }
}
