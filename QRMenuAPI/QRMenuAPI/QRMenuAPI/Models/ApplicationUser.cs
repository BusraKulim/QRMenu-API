﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace QRMenuAPI.Models
{
	public class ApplicationUser:IdentityUser
	{
        [StringLength(100, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(100)")]
        public override string UserName { get; set; }

        public string Name { get; set; } = "";
        [EmailAddress]
        [StringLength(100, MinimumLength = 5)]
        [Column(TypeName = "varchar(100)")]
        public override string Email { get; set; }

        public override string? PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }

        public int CompanyId { get; set; }

        public byte StateId { get; set; }
        [ForeignKey("StateId")]
        public State? State { get; set; }
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

        public int RestaurantId { get; set; }
        //public override string Email { get => base.Email; set => base.Email = value; }

    }
}

