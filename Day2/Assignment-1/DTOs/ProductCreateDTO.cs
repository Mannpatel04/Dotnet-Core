using System.ComponentModel.DataAnnotations;

namespace Assignment_1.DTOs
{
    public class ProductCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
