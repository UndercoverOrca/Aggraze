namespace Aggraze.Domain;

public record MetricTable(
    string Title,
    List<int> Years,
    string SummaryLabel,
    Dictionary<(int? Year, string Month), double> Values);