using Knoex.Models;
using System.Text.RegularExpressions;
using Markdig;
namespace Knoex.ViewModels
{
    public class PostViewModel : BaseViewModel
    {
        public Post Post;
        public string Timestamp => GetTimestamp(Post.CreatedAt);
        public string GetMarkdownHtml() => Markdown.ToHtml(Post.Body ?? "");
        public string Summary()
        {
            string content = Regex.Replace(GetMarkdownHtml(), "<.*?>|&.*?;", string.Empty);
            content = content.Replace("\n", "").Replace("\r", "");

            return !String.IsNullOrWhiteSpace(content) && content.Length > 150
                ? content[..150] + "..."
                : content;
        }
    }
}