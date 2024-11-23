using System.ComponentModel.DataAnnotations;

public class Posts
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    [Required]
    public string Title { get; set; }
    [Required]
    public string MainImage { get; set; }
    [Required]
    public string Caption { get; set; }
    [Required]
    public string Body { get; set; }
    [Required]
    public List<string> WordKey { get; set; }
}