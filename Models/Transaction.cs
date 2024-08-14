using System.ComponentModel.DataAnnotations;

namespace CelsiaAssetsment.Models
{
    public class Transaction
    {
        [Required]
        public int Id { get; set; }
    }
}