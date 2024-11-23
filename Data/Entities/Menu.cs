using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Menu
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    public int? ParentId { get; set; }
    public Menu? Parent { get; set; }
    public List<Menu> Children { get; set; }

    [Required]
    public bool EndLine { get; set; }

    public List<Category> Subs { get; set; }
}