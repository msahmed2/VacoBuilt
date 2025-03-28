using VacoBuilt.Entities;

namespace VacoBuilt.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}
