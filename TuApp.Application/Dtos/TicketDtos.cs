namespace TuApp.Application.Dtos;

public record TicketDto(string TicketId, string UserId, string Title, string? Description, string Status, DateTime? CreatedAt, DateTime? ClosedAt);
public record CreateTicketDto(string UserId, string Title, string? Description);
public record UpdateTicketDto(string Title, string? Description, string Status);
