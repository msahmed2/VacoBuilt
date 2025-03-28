namespace VacoBuilt.Dtos.Read
{
    public class BlogPostResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Contents { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public int CategoryId { get; set; }
    }
}
