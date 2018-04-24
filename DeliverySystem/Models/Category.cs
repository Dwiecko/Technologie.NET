using DeliverySystem.Attributes;
using DeliverySystem.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySystem.Models
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings =false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Capitalize]
        [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Please use name of length in range 3..30")]

        public string Name { get; set; }

        public virtual ICollection<Delivery> Delivery { get; set; }
    }
}
