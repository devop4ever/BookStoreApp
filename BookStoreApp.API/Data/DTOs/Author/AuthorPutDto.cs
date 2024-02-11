using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Data.DTOs.Author;

public class AuthorPutDto : BaseDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    public string? Bio { get; set; }
}
