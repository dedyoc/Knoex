using System.Text.RegularExpressions;
using Knoex.Models;

namespace Knoex.ViewModels
{
    public class PostViewModel
    {
        public Post Post;
        public string Summary()
        {
            string content = Post.Body;
            // Remove markdown elements
            content = Regex.Replace(content, "/\n={2,}/g", "\n");
            content = Regex.Replace(content, "/~~/g", string.Empty);
            content = Regex.Replace(content, "/`{3}.*\n/g", string.Empty);
            content = Regex.Replace(content, "/<[^>]*>/g", string.Empty);
            content = Regex.Replace(content, "/^[=\\-]{2,}\\s*$/g", string.Empty);
            content = Regex.Replace(content, "/\\[\\^.+?\\](\\: .*?$)?/g", string.Empty);
            content = Regex.Replace(content, "/\\s{0,2}\\[.*?\\]: .*?$/g", string.Empty);
            content = Regex.Replace(content, "/\\!\\[.*?\\][\\[\\(].*?[\\]\\)]/g", string.Empty);
            content = Regex.Replace(content, "/\\[(.*?)\\][\\[\\(].*?[\\]\\)]/g", "$1");
            return !string.IsNullOrWhiteSpace(content) && content.Length > 200
                ? content[..200] + "..."
                : content;
        }
    }
}