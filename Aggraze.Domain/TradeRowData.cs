using LanguageExt;

namespace Aggraze.Domain;

public class TradeRowData
{
    public Option<string> Pair {get; init; }
    public Option<DateOnly> Date {get; init; }
    public Option<TimeOnly> OpenTime {get; init; }
    public Option<TimeOnly> ClosingTime {get; init; }
    public Option<OrderType> OrderType {get; init; }
    public Option<string> Day {get; init; }
    public Option<string> Session {get; init; }
    public Option<decimal> Pobc {get; init; }
    public Option<bool> News {get; init; }
    public Option<Result> Result {get; init; }
    public Option<decimal> Mutation {get; init; }
    public Option<decimal> MaximumDrawdown {get; init; }
    public Option<decimal> MaximumResult {get; init; }
    public Option<DateOnly> DateOfCreatedLevel {get; init; }
    public Option<TimeOnly> TimeOfCreatedLevel {get; init; }
    public Option<decimal> LevelPrice {get; init; }
    public Option<Direction> FourHourDirection {get; init; }
    public Option<bool> Closed15MinInOppositeDirection {get; init; }
    public Option<decimal> MaximumDrawdownWithRunner {get; init; }
    public Option<decimal> Open {get; init; }
    public Option<decimal> High {get; init; }
    public Option<decimal> Low {get; init; }
    public Option<decimal> Close {get; init; }
    public Option<decimal> NextLevelPrice { get; init; }
}
