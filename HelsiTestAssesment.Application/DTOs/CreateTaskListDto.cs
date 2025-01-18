namespace HelsiTestAssesment.Application.DTOs;

public class CreateTaskListDto
{
    public string Name { get; set; } = default!;

    public IEnumerable<string>? AccessibleUserIds { get; set; }

    public IEnumerable<string>? Tasks { get; set; }

}
