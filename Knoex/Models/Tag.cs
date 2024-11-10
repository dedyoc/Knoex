namespace Knoex.Models
{
    public class Tag : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public Tag()
        {
            Posts = new HashSet<Post>();
        }
    }
}