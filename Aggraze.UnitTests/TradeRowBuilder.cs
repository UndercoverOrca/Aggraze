using Aggraze.Domain.Types;

namespace Aggraze.UnitTests;

public class TradeRowBuilder
{
    private readonly List<TradeRow> _rows = [];
    private DateOnly _date;
    private TradeRowData _data = new();

    public TradeRowBuilder Add(
        DateOnly date,
        TradeRowData data)
    {
        _rows.Add(new TradeRow(date, data));
        return this;
    }

    public TradeRowBuilder AddDefault()
    {
        _rows.Add(new TradeRow(
            new DateOnly(2025, 1, 1),
            new TradeRowData
            {
                OpenTime = new TimeOnly(9, 0, 0),
                ClosingTime = new TimeOnly(10, 0, 0),
                Date = new DateOnly(2025, 1, 1),
                Mutation = 1
            }));
        return this;
    }
    
    public TradeRowBuilder SetDate(DateOnly date)
    {
        _date = date;
        return this;
    }

    public TradeRowBuilder AddOpenTime(TimeOnly openTime)
    {
        _data.OpenTime = openTime;
        return this;
    }

    public TradeRowBuilder AddClosingTime(TimeOnly closingTime)
    {
        _data.ClosingTime = closingTime;
        return this;
    }

    public List<TradeRow> Build() =>
        _rows;
}