public class DCategoryClient
{
    public int Id { get; set; }
    public int ParentMenuId { get; set; }
    public string ParentMenuTitle { get; set; }
    public string Title { get; set; }
    public List<DPost>? Posts { get; set; } 

    static public DCategoryClient FromCategory(Category category , bool takes){
        if (category == null) return null ;
        return new DCategoryClient{
            Id = category.Id,
            ParentMenuId = category.ParentMenu.Id,
            ParentMenuTitle = category.ParentMenu.Title,
            Title = category.Title,
            Posts = takes ? category.Posts.OrderByDescending(x=>x.Id).Take(6).Select(DPost.FromPost).ToList() : category.Posts.OrderByDescending(x=>x.Id).Select(DPost.FromPost).ToList()
        };
    }
}