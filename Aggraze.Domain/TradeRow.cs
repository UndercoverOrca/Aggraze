namespace Aggraze.Domain;

public record TradeRow(
    DateOnly Date,
    IReadOnlyDictionary<string, string> Value);