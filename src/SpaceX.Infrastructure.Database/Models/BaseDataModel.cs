namespace SpaceX.Infrastructure.Database.Models;

public class BaseDataModel
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }
}

