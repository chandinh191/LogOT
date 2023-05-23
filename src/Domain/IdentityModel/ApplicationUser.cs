using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace LogOT.Domain.IdentityModel;
public class ApplicationUser : IdentityUser
{
    [Column("Fullname")]
    [Display(Name = "Fullname")]
    [StringLength(20, ErrorMessage = ("Full name must be less than 20 characters"))]
    [RegularExpression(@"^[\p{L}0-9_]*$", ErrorMessage = "Full name is required")]
    [Required(ErrorMessage = "employee Full name is required")]
    public string Fullname { get; set; }
    [Column("Address")]
    [Display(Name = " Address")]
    [StringLength(30, ErrorMessage = ("Address must be less than 20 characters"))]
    [Required(ErrorMessage = "employee Address is required")]
    public string Address { get; set; }
    [Column("Image")]
    [Display(Name = " Image")]
    [StringLength(500, ErrorMessage = ("Image must be less than 500 characters"))]
    [Required(ErrorMessage = "employee Image is required")]
    public string Image { get; set; }
    public virtual Employee Employee { get; set; }

}
