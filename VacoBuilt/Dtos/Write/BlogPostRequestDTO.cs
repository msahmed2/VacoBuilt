using System.ComponentModel.DataAnnotations;

namespace VacoBuilt.Dtos.Write
{
    public class BlogPostRequestDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Contents { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
