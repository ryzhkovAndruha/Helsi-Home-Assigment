using System.ComponentModel.DataAnnotations;

namespace HelsiTestAssesment.Infrastucture.Options;

public class MongoOptions
{
    public const string SectionName = "Mongo";

    [Required]
    public string ConnectionString { get; set; } = string.Empty;

    [Required]
    public string DefaultDatabaseName { get; set; } = string.Empty;
}
