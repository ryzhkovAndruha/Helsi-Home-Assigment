namespace HelsiTestAssesment.Application.DTOs;

public class UpdateTaskListDto
{
    public string Id { get; set; } = default!;

    public string Name { get; set; } = default!;

    public IEnumerable<string>? Tasks { get; set; }
}
