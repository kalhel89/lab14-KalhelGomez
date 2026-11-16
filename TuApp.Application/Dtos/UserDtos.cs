namespace TuApp.Application.Dtos;

public record UserDto(string UserId, string Username, string? Email, DateTime? CreatedAt);
public record RegisterUserDto(string Username, string Password, string? Email);
public record UpdateUserDto(string Username, string? Email);
