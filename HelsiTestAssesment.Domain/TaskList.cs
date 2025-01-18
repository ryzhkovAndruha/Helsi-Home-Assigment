using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HelsiTestAssesment.Domain;

public class TaskList
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string Id { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string OwnerId { get; set; } = default!;

    public IEnumerable<string>? Tasks { get; set; }

    public IEnumerable<string>? AccessibleUserIds { get; set; }

    public DateTime CreatedAt { get; set; }
}
