namespace HelsiTestAssesment.Application.DTOs;

public class CommandOperationResultDto
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public object? Data { get; set; }
}
