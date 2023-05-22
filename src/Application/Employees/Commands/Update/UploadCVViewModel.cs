using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace LogOT.Application.Employees.Commands.Update;
public class UploadCVViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Full Name")]
    public string FullName { get; set; }

    [Display(Name = "CV File")]
    [Required(ErrorMessage = "Please select a CV file.")]
    [DataType(DataType.Upload)]
    public IFormFile CVFile { get; set; }
}
