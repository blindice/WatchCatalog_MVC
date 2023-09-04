using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WatchCatalog_MVC.ViewModels
{
    public class WatchDetailsViewModel
    {
        public int WatchId { get; set; }

        public string Image { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string WatchName { get; set; } = null!;
        [StringLength(500)]
        public string Short_description { get; set; } = null!;
        public string Full_Description { get; set; } = null!;
        [Column(TypeName = "decimal(13, 2)")]
        [Range(1.0, double.MaxValue, ErrorMessage = "Price must be atleast ฿1")]
        public decimal Price { get; set; }
        [StringLength(50)]
        public string Chronograph { get; set; } = null!;
        [StringLength(30)]
        public string Caliber { get; set; } = null!;
        [Column(TypeName = "decimal(8, 2)")]
        [Range(1.0, 1000, ErrorMessage = "Weight must be between 1g and 1kg")]
        public decimal Weight { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        [Range(1.0, 50, ErrorMessage = "Height must be between 1mm and 50mm")]
        public decimal Height { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        [Range(1.0, 50, ErrorMessage = "Diameter must be between 1mm and 50mm")]
        public decimal Diameter { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        [Range(1.0, 20, ErrorMessage = "Height must be between 1mm and 20mm")]
        public decimal Thickness { get; set; }
        [StringLength(30)]
        public string Jewel { get; set; } = null!;
        [StringLength(30)]
        public string Case { get; set; } = null!;
        [StringLength(30)]
        public string Strap { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }

        public string ToggleButtonName() => IsActive ? "Disable" : "Enable";

        public string ToggleButtonClass() => IsActive ? "btn btn-outline-danger" : "btn btn-outline-success";

        public string GetProductAvailabilityClass() => IsActive ? string.Empty : "not-available";
    }
}
