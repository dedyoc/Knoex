using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using NpgsqlTypes;

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
        public int VoteScore { get; set; } = 0;
        public int ViewCount { get; set; } = 0;
        public int AnswersCount { get; set; } = 0;
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<View> Views { get; set; }
        public NpgsqlTsVector SearchVector { get; set; }

        public Post()
        {
            Tags = new HashSet<Tag>();
            Comments = new HashSet<Comment>();
            Answers = new HashSet<Post>();
            Votes = new HashSet<Vote>();
            Views = new HashSet<View>();
        }
    }
}