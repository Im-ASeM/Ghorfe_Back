using System.ComponentModel.DataAnnotations;
public class PhoneNumber
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Phone { get; set; }
}