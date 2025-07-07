using Aggraze.Domain;

namespace Aggraze.Infrastructure;

public static class TradeRowDataMapper
{
    public static TradeRowData Map(Dictionary<string, string> data) =>
        new()
        {
            Pair = data.TryGetValue<string>("Pair"),
            Date = data.TryGetValue<DateOnly>("Date"),
            OpenTime = data.TryGetValue<TimeOnly>("Open Time"),
            ClosingTime = data.TryGetValue<TimeOnly>("Closing Time"),
            OrderType = data.TryGetValue<OrderType>("Order"),
            Day = data.TryGetValue<string>("Day"),
            Session = data.TryGetValue<string>("Session"),
            Pobc = data.TryGetValue<decimal>("PoBC"),
            News = data.TryGetValue<bool>("News"),
            Result = data.TryGetValue<Result>("Result"),
            Mutation = data.TryGetValue<decimal>("Mutation"),
            MaximumDrawdown = data.TryGetValue<decimal>("Max. Drawdown"),
            MaximumResult = data.TryGetValue<decimal>("Max. Result"),
            DateOfCreatedLevel = data.TryGetValue<DateOnly>("DoL"),
            TimeOfCreatedLevel = data.TryGetValue<TimeOnly>("ToL"),
            LevelPrice = data.TryGetValue<decimal>("Level price"),
            FourHourDirection = data.TryGetValue<Direction>("4H Direction"),
            Closed15MinInOppositeDirection = data.TryGetValue<bool>("15MOC"),
            Open = data.TryGetValue<decimal>("Open"),
            High = data.TryGetValue<decimal>("High"),
            Low = data.TryGetValue<decimal>("Low"),
            Close = data.TryGetValue<decimal>("Close"),
            NextLevelPrice = data.TryGetValue<decimal>("NLP"),
        };
}