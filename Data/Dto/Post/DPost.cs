using System.Data.Common;

public class DPost
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public string Title { get; set; }
    public string MainImage { get; set; }
    public string Caption { get; set; }
    public string Body { get; set; }
    public List<string> WordKey { get; set; }

    static public DPost FromPost(Posts post)
    {
        if (post == null) return null;
        return new DPost{
            Id = post.Id,
            Body = post.Body,
            Caption = post.Caption,
            CategoryId = post.Category.Id,
            CategoryTitle = post.Category.Title,
            MainImage = post.MainImage,
            Title = post.Title,
            WordKey = post.WordKey
        };
    }
}