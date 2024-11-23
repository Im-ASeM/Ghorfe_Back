public class DMenuAdmin
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<DMenuAdmin>? Children { get; set; }
    public bool EndLine { get; set; }
    public List<DCategory>? Categories { get; set; }

    static public DMenuAdmin FromMenu(Menu menu)
    {
        if (menu == null) return null;
        return new DMenuAdmin{
            Id=menu.Id,
            Title=menu.Title,
            Children = menu.Children?.Select(FromMenu).ToList(),
            EndLine = menu.EndLine,
            Categories = menu.Subs?.Select(DCategory.FromCategory).ToList()
        };
    }
}