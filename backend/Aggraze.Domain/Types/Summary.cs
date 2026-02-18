namespace Aggraze.Domain.Types;

public record Summary<T>(
    SummaryType SummaryType,
    Dictionary<string, T> Data) : ISummary
{
    Dictionary<string, object> ISummary.Data
        => Data.ToDictionary(kv => kv.Key, kv => (object)kv.Value!);
}