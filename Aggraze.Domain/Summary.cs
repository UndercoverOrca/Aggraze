namespace Aggraze.Domain;

public record Summary(
    SummaryType SummaryType,
    Dictionary<string, TimeSpan> data);