using System.Data.Common;

public class DMiniPost
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public string Title { get; set; }
    public string MainImage { get; set; }
    public string Caption { get; set; }
    public List<string> WordKey { get; set; }

    static public DMiniPost FromPost(Posts post)
    {
        if (post == null) return null;
        return new DMiniPost{
            Id = post.Id,
            Caption = post.Caption,
            CategoryId = post.Category.Id,
            CategoryTitle = post.Category.Title,
            MainImage = post.MainImage,
            Title = post.Title,
            WordKey = post.WordKey
        };
    }
}