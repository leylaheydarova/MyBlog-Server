namespace MyBlog.App.DTOs.Comment
{
    public class CommentCreateDto
    {
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
    }
}
