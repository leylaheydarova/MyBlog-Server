using MyBlog.App.Models.BaseModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.App.Models
{
    public class AppUser : BaseEntity
    {
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
