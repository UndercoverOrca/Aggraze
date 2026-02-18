namespace Aggraze.Domain.Types;

public interface ISummary
{
    SummaryType SummaryType { get; }
    Dictionary<string, object> Data { get; }
}