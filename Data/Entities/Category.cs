using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ParentMenuId { get; set; }
    public Menu ParentMenu { get; set; }

    [Required]
    public string Title { get; set; }

    public List<Posts> Posts { get; set; }

}