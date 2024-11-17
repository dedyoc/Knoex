using System.ComponentModel.DataAnnotations.Schema;

namespace Knoex.Models
{
    public class Comment : BaseModel
    {
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
        [Column(TypeName = "text")]
        public string Body { get; set; }
    }
}