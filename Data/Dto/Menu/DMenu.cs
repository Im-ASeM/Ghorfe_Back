public class DMenu
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<DMenu>? Children { get; set; }
    public bool EndLine { get; set; }

    static public DMenu FromMenu(Menu menu)
    {
        if (menu == null) return null;
        return new DMenu{
            Id=menu.Id,
            Title=menu.Title,
            Children = menu.Children?.Select(FromMenu).ToList(),
            EndLine = menu.EndLine
        };
    }
}