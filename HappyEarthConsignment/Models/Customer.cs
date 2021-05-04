﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HappyEarthConsignment.Models
{
    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        //[RegularExpression(@"^([\w-\.]+)@([\w]+)\.([a-zA-Z]{2,4})$", ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Shipping Address")]
        public string StreetAddress { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Created { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Updated { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(25)]
        public string Password { get; set; }

        [InverseProperty(nameof(Order.Customer))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}