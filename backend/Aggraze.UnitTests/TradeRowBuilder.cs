using Aggraze.Domain.Types;

namespace Aggraze.UnitTests;

public class TradeRowBuilder
{
    private readonly List<TradeRow> _rows = [];

    public RowBuilder AddRow() => new(this);

    public List<TradeRow> Build() => _rows;

    public class RowBuilder
    {
        private readonly TradeRowBuilder _parent;
        private DateOnly _date;
        private TradeRowData _data = new();

        public RowBuilder(TradeRowBuilder parent) =>
            _parent = parent;

        public RowBuilder SetDate(DateOnly date)
        {
            _date = date;
            return this;
        }

        public RowBuilder AddOpenTime(TimeOnly openTime)
        {
            _data.OpenTime = openTime;
            return this;
        }

        public RowBuilder AddClosingTime(TimeOnly closingTime)
        {
            _data.ClosingTime = closingTime;
            return this;
        }

        public RowBuilder AddMutation(int mutation)
        {
            _data.Mutation = mutation;
            return this;
        }

        public TradeRowBuilder Add()
        {
            _parent._rows.Add(new TradeRow(_date, _data));
            return _parent;
        }
    }
}