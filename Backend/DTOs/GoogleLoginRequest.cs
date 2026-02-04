using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public record GoogleLoginRequest(
    [Required(ErrorMessage = "IdToken is required.")]
    [MinLength(1, ErrorMessage = "IdToken cannot be empty.")]
    string IdToken
);
