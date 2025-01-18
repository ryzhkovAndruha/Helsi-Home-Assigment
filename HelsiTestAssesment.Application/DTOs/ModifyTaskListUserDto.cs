namespace HelsiTestAssesment.Application.DTOs;

public class ModifyTaskListUserDto
{
    public string TaskListId { get; set; } = default!;

    public string UserToAdd { get; set; } = default!;
}
