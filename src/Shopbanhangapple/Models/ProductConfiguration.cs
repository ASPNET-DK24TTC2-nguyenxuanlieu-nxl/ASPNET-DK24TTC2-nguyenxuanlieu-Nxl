using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopbanhangapple.Models
{
    public class ProductConfiguration
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        
        public string OptionType { get; set; } = string.Empty;
        public string OptionValue { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAdjustment { get; set; }
    }
}
