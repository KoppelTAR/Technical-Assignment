using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static TechnicalAssignment.Models.SavedUsers;

namespace TechnicalAssignment.Models
{
    public class SavedUsers
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Sectors { get; set; }
        [Required]
        [BooleanMustBeTrue(ErrorMessage = "You must Agree to the terms")]
        public bool ToS { get; set; }

        public class BooleanMustBeTrue : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return value is bool && (bool)value;
            }
        }
    }

}
