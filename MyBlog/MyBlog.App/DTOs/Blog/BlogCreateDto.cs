namespace MyBlog.App.DTOs.Blog
{
    public class BlogCreateDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
    }
}
