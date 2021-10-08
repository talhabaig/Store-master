using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.Models
{
    public class ManufacturerViewModel
    {
        public int ManufactId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Alias { get; set; }

        [RegularExpression(@"^((\+92)|(0092))-{0,1}\d{3}-{0,1}\d{7}$|^\d{11}$|^\d{4}-\d{7}$", ErrorMessage = "Please enter in correct Format")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string EditManufacturerUrl { get; set; }
    }
}