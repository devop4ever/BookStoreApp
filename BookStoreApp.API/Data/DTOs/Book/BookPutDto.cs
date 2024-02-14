using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Data.DTOs.Book;

public class BookPutDto : BaseDto
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [Range(1455, int.MaxValue)]
    public int? Year { get; set; }
    public string? Summary { get; set; }

    public string? Image { get; set; }

    public decimal? Price { get; set; }

    public int AuthorId { get; set; }

    public string AuthorName { get; set; } = string.Empty;
}
