using System.ComponentModel.DataAnnotations;

public class Slider
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string SliderName { get; set; }

    [Required]
    public int SliderType { get; set; } // 1 Big Slide // 2 Small Slide

    [Required]
    public bool active { get; set; }

}