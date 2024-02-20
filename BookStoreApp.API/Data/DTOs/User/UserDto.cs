using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Data.DTOs.User;

public class UserDto : LoginUserDto
{

    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;

    public string Role {  get; set; } = string.Empty;


}
