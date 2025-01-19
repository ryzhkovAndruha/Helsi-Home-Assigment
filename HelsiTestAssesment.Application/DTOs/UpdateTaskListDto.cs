namespace HelsiTestAssesment.Application.DTOs;

public class UpdateTaskListDto
{
    public string Name { get; set; } = default!;

    public IEnumerable<string>? Tasks { get; set; }
}
