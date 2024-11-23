public class DCategoryAdmin
{
    public int Id { get; set; }
    public int ParentMenuId { get; set; }
    public string ParentMenuTitle { get; set; }
    public string Title { get; set; }
    public int PostCount { get; set; }
    public List<DMiniPost>? Posts { get; set; }

    static public DCategoryAdmin FromCategory(Category? category)
    {
        if (category == null) return null;
        return new DCategoryAdmin
        {
            Id = category.Id,
            ParentMenuId = category.ParentMenu.Id,
            ParentMenuTitle = category.ParentMenu.Title,
            Title = category.Title,
            PostCount = category.Posts.Count,
            Posts = category.Posts?.Select(DMiniPost.FromPost).ToList()
        };
    }
}