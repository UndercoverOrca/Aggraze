namespace Aggraze.Domain.Types;

public record Summary(
    SummaryType SummaryType,
    Dictionary<string, TimeSpan> Data);