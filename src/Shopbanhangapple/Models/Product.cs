using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopbanhangapple.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string TechnicalSpecs { get; set; } = string.Empty;
        public int Stock { get; set; }
        public bool IsCustomizable { get; set; }
        
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
        public List<ProductConfiguration> Configurations { get; set; } = new();
    }
}
