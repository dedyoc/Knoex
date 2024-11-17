using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Knoex.Models
{
    public enum PostType
    {
        Question = 1,
        Answer = 2,
    }
    public class Post : BaseModel
    {
        public PostType Type { get; set; }
        [AllowNull]
        public int? ParentId { get; set; }
        [AllowNull]
        public virtual Post Parent { get; set; }
        [InverseProperty("Parent")]
        public virtual ICollection<Post> Answers { get; set; }
        [AllowNull]
        public int? AcceptedAnswerId { get; set; }
        [AllowNull]
        public virtual Post AcceptedAnswer { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [Column(TypeName = "text")]
        public string Body { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Post()
        {
            Tags = new HashSet<Tag>();
            Answers = new HashSet<Post>();
        }
    }
}