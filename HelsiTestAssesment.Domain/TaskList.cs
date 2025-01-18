namespace HelsiTestAssesment.Domain;

public class TaskList
{
    public string Id { get; set; }

    public string Name { get; set; } = default!;

    public string OwnerId { get; set; } = default!;

    public IEnumerable<string>? AccessibleUserIds { get; set; }

    public DateTime CreatedAt { get; set; }
}
