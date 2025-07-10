using LanguageExt;

namespace Aggraze.Domain.Types;

public class TradeRowData
{
    public Option<string> Pair {get; set; }
    public Option<DateOnly> Date {get; set; }
    public Option<TimeOnly> OpenTime {get; set; }
    public Option<TimeOnly> ClosingTime {get; set; }
    public Option<OrderType> OrderType {get; set; }
    public Option<string> Day {get; set; }
    public Option<string> Session {get; set; }
    public Option<decimal> Pobc {get; set; }
    public Option<bool> News {get; set; }
    public Option<Result> Result {get; set; }
    public Option<decimal> Mutation {get; set; }
    public Option<decimal> MaximumDrawdown {get; set; }
    public Option<decimal> MaximumResult {get; set; }
    public Option<DateOnly> DateOfCreatedLevel {get; set; }
    public Option<TimeOnly> TimeOfCreatedLevel {get; set; }
    public Option<decimal> LevelPrice {get; set; }
    public Option<Direction> FourHourDirection {get; set; }
    public Option<bool> Closed15MinInOppositeDirection {get; set; }
    public Option<decimal> MaximumDrawdownWithRunner {get; set; }
    public Option<decimal> Open {get; set; }
    public Option<decimal> High {get; set; }
    public Option<decimal> Low {get; set; }
    public Option<decimal> Close {get; set; }
    public Option<decimal> NextLevelPrice { get; set; }
}
