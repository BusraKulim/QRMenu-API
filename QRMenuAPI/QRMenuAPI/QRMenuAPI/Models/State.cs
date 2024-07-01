using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRMenuAPI.Models
{
    public class State
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]  // Db otomatik Id atamaması için EF kodu. Byte Id'leri otomatik Primary Key olarak görmez
        public byte Id { get; set; }
        [Required] //boş enter basamaz değer girmek zorunda
        [StringLength(10)] //en fazla 10 karakter girebilir
        [Column(TypeName = "nvarchar(10)")]
        public string Name { get; set; } = "";
    }
}

