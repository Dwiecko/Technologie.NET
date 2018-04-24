using DeliverySystem.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliverySystem.Models.ProductViewModels
{
    public class ProductViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Capitalize]
        [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Please use name of length in range 3..30")]
        public string Name { get; set; }

        public virtual ICollection<Delivery> Delivery { get; set; }
    }
}
