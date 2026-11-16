namespace TuApp.Application.Dtos;

public record ResponseDto(string ResponseId, string TicketId, string ResponderId, string Message, DateTime? CreatedAt);
public record CreateResponseDto(string TicketId, string ResponderId, string Message);
