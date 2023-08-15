using System.ComponentModel.DataAnnotations;

namespace WatchCatalog_MVC.DTOs
{
    public class ToggleWatchDTO
    {
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Id must be numeric")]
        public int WatchId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
