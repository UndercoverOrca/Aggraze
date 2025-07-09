namespace Aggraze.Domain;

public record TradeRow(
    DateOnly Date,
    TradeRowData Data);