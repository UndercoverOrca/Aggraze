using Aggraze.Domain.Extensions;
using Aggraze.Domain.Types;

namespace Aggraze.Infrastructure;

public static class TradeRowDataMapper
{
    public static TradeRowData Map(Dictionary<string, string> data) =>
        new()
        {
            Pair = data.GetValueOrNone("Pair"),
            Date = data.GetValueOrNone("Date").MapIfSome(x => DateOnly.FromDateTime(DateTime.Parse(x))),
            OpenTime = data.GetValueOrNone("Open time").MapIfSome(x => TimeOnly.FromDateTime(DateTime.Parse(x))),
            ClosingTime = data.GetValueOrNone("Closing time").MapIfSome(x => TimeOnly.FromDateTime(DateTime.Parse(x))),
            OrderType = data.GetValueOrNone("Order").MapIfSome(Enum.Parse<OrderType>),
            Day = data.GetValueOrNone("Day"),
            Session = data.GetValueOrNone("Session"),
            Pobc = data.GetValueOrNone("PoBC").Bind(x => Pips.TryCreate(x).ToOption()),
            News = data.GetValueOrNone("News").MapIfSome(bool.Parse),
            Result = data.GetValueOrNone("Result").MapIfSome(Enum.Parse<Result>),
            Mutation = data.GetValueOrNone("Mutation").MapIfSome(x => decimal.Parse(x) * 100),
            MaximumDrawdown = data.GetValueOrNone("Max. drawdown").Bind(x => Pips.TryCreate(x).ToOption()),
            MaximumResult = data.GetValueOrNone("Max. result").MapIfSome(decimal.Parse),
            DateOfCreatedLevel = data.GetValueOrNone("DoL").MapIfSome(x => DateOnly.FromDateTime(DateTime.Parse(x))),
            TimeOfCreatedLevel = data.GetValueOrNone("ToL").MapIfSome(x => TimeOnly.FromDateTime(DateTime.Parse(x))),
            LevelPrice = data.GetValueOrNone("Level price").MapIfSome(decimal.Parse),
            FourHourDirection = data.GetValueOrNone("4H direction").MapIfSome(Enum.Parse<Direction>),
            Closed15MinInOppositeDirection = data.GetValueOrNone("15MOC").MapIfSome(bool.Parse),
            Open = data.GetValueOrNone("Open").MapIfSome(decimal.Parse),
            High = data.GetValueOrNone("High").MapIfSome(decimal.Parse),
            Low = data.GetValueOrNone("Low").MapIfSome(decimal.Parse),
            Close = data.GetValueOrNone("Close").MapIfSome(decimal.Parse),
            NextLevelPrice = data.GetValueOrNone("NLP").MapIfSome(decimal.Parse),
            MaximumDrawdownWithRunner = data.GetValueOrNone("MDwR").Bind(x => Pips.TryCreate(x).ToOption())
        };
}