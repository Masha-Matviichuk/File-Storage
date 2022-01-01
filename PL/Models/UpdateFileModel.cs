using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class UpdateFileModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public  int AccessId { get; set; }
    }
}