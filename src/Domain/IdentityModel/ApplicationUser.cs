﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LogOT.Domain.IdentityModel;
public class ApplicationUser : IdentityUser
{
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string Image { get; set; }
    public virtual Employee Employee { get; set; }

}
