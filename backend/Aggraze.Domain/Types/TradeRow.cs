namespace Aggraze.Domain.Types;

public record TradeRow(
    DateOnly Date,
    TradeRowData Data);