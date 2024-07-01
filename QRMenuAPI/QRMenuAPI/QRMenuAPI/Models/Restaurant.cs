using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QRMenuAPI.Models;

namespace QRMenuAPI.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = "";

        public int CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }

        [Column(TypeName = "tinyint")]
        public byte StateId { get; set; }
        [ForeignKey("StateId")]
        public State? State { get; set; }

        [Phone]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public int Phone { get; set; }

        //public List<Category>? Categories { get; set; }

        [StringLength(5, MinimumLength = 5)]
        [Column(TypeName = "char(5)")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; } = "";

        [StringLength(200, MinimumLength = 5)]
        [Column(TypeName = "nvarchar(200)")]
        public string Address { get; set; } = "";

        [Column(TypeName = "smalldatetime")]
        public DateTime RegisterDate { get; set; }

        //public virtual List<User>? Users { get; set; }
    }
}

