using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Data.DTOs.Book;

public class BookPostDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Range(1455, int.MaxValue)]
    public int Year { get; set; }

    [Required]
    public string Isbn { get; set; } = string.Empty;

    public string? Summary { get; set; } 

    public string Image { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }


}
