﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WatchCatalog_MVC.DTOs
{
    public class UpdateWatchDTO
    {
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Id must be numeric")]
        public int WatchId { get; set; }

        public IFormFile? Image { get; set; }

        [StringLength(20)]
        public string WatchName { get; set; } = null!;
        [StringLength(50)]
        public string Short_description { get; set; } = null!;
        [StringLength(100)]
        public string Full_Description { get; set; } = null!;
        [Column(TypeName = "decimal(13, 4)")]
        [Range(1.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1} and less than {2}.")]
        public decimal Price { get; set; }
        [StringLength(50)]
        public string Chronograph { get; set; } = null!;
        [StringLength(30)]
        public string Caliber { get; set; } = null!;
        [Column(TypeName = "decimal(8, 2)")]
        [Range(1.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1} and less than {2}.")]
        public decimal Weight { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        [Range(1.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1} and less than {2}.")]
        public decimal Height { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        [Range(1.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1} and less than {2}.")]
        public decimal Diameter { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        [Range(1.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1} and less than {2}.")]
        public decimal Thickness { get; set; }
        [StringLength(30)]
        public string Jewel { get; set; } = null!;
        [StringLength(30)]
        public string Case { get; set; } = null!;
        [StringLength(30)]
        public string Strap { get; set; } = null!;

        [Required]
        public bool? IsActive { get; set; }
    }
}