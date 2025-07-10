using Aggraze.Domain.Extensions;
using Aggraze.Domain.Types;

namespace Aggraze.Infrastructure;

public static class TradeRowDataMapper
{
    public static TradeRowData Map(Dictionary<string, string> data) =>
        new()
        {
            Pair = data.TryGetValueOrDefault("Pair"),
            Date = data.TryGetValueOrDefault("Date").MapIfSome(x => DateOnly.FromDateTime(DateTime.Parse(x))),
            OpenTime = data.TryGetValueOrDefault("Open time").MapIfSome(x => TimeOnly.FromDateTime(DateTime.Parse(x))),
            ClosingTime = data.TryGetValueOrDefault("Closing time").MapIfSome(x => TimeOnly.FromDateTime(DateTime.Parse(x))),
            OrderType = data.TryGetValueOrDefault("Order").MapIfSome(Enum.Parse<OrderType>),
            Day = data.TryGetValueOrDefault("Day"),
            Session = data.TryGetValueOrDefault("Session"),
            Pobc = data.TryGetValueOrDefault("PoBC").MapIfSome(decimal.Parse),
            News = data.TryGetValueOrDefault("News").MapIfSome(bool.Parse),
            Result = data.TryGetValueOrDefault("Result").MapIfSome(Enum.Parse<Result>),
            Mutation = data.TryGetValueOrDefault("Mutation").MapIfSome(x => decimal.Parse(x) * 100),
            MaximumDrawdown = data.TryGetValueOrDefault("Max. Drawdown").MapIfSome(decimal.Parse),
            MaximumResult = data.TryGetValueOrDefault("Max. Result").MapIfSome(decimal.Parse),
            DateOfCreatedLevel = data.TryGetValueOrDefault("DoL").MapIfSome(x=> DateOnly.FromDateTime(DateTime.Parse(x))),
            TimeOfCreatedLevel = data.TryGetValueOrDefault("ToL").MapIfSome(x=> TimeOnly.FromDateTime(DateTime.Parse(x))),
            LevelPrice = data.TryGetValueOrDefault("Level price").MapIfSome(decimal.Parse),
            FourHourDirection = data.TryGetValueOrDefault("4H direction").MapIfSome(Enum.Parse<Direction>),
            Closed15MinInOppositeDirection = data.TryGetValueOrDefault("15MOC").MapIfSome(bool.Parse),
            Open = data.TryGetValueOrDefault("Open").MapIfSome(decimal.Parse),
            High = data.TryGetValueOrDefault("High").MapIfSome(decimal.Parse),
            Low = data.TryGetValueOrDefault("Low").MapIfSome(decimal.Parse),
            Close = data.TryGetValueOrDefault("Close").MapIfSome(decimal.Parse),
            NextLevelPrice = data.TryGetValueOrDefault("NLP").MapIfSome(decimal.Parse),
            MaximumDrawdownWithRunner = data.TryGetValueOrDefault("MDwR").MapIfSome(decimal.Parse)
        };
}