// referencing demo 7
// Defines a shopping cart item object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//add namespace

using System.ComponentModel.DataAnnotations;

namespace HappyEarthConsignment.Models
{
    public class CartItem
    {
        public Product Product { get; set; }

        [Required(ErrorMessage = "Please enter quantity")]
        [Range(0, 1, ErrorMessage = "Only one of each product available")]
        public int Quantity { get; set; }
    }
}
