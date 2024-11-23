using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
public class Logo
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string ImageLogo { get; set; }
    [Required]
    public bool active { get; set; }
}