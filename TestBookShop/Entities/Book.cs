using System.ComponentModel.DataAnnotations;

namespace TestBookShop.Entities;

public class Book
{
    [Key] [Required] public int Id { get; set; }
    [Required] [MaxLength(100)] public string Title { get; set; } = default!;
    [Required] public DateTime DateOfRelease { get; set; }
}