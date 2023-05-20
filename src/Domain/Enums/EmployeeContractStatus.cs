﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Enums;
public enum EmployeeContractStatus
{
    //còn hiệu lực
    Effective = 0,
    //hết hiệu lực
    Expired = 1,
    //đang được gia hạn
    Renewed = 2,
    //đã hủy
    Cancelled = 3
}
