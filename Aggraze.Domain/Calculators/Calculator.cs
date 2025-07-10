using System.Globalization;
using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public class Calculator
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
    
    protected static Dictionary<(int Year, int Month), IEnumerable<TradeRowData>> GroupTradesByYearAndMonth(IEnumerable<TradeRow> trades) =>
        trades
            .GroupBy(x => (x.Date.Year, x.Date.Month))
            .ToDictionary(
                group => group.Key,
                group => group.Select(y => y.Data));
    
    protected static void AddYearMonthSummary<T>(
        Dictionary<int, Dictionary<string, T>> yearMonthData,
        Dictionary<string, List<T>> summaryHelper,
        (int Year, int Month) group,
        T value)
    {
        var year = group.Year;
        var month= Culture.DateTimeFormat.GetMonthName(group.Month);
        
        if (!yearMonthData.ContainsKey(year))
        {
            yearMonthData[year] = new Dictionary<string, T>();
        }

        yearMonthData[year][month] = value;

        if (!summaryHelper.ContainsKey(month))
        {
            summaryHelper[month] = [];
        }

        summaryHelper[month].AddRange(value);
    }

    protected static Dictionary<int,Dictionary<string,TimeSpan>> OrderYearMonthDataByYear(Dictionary<int,Dictionary<string,TimeSpan>> yearMonthData) =>
        yearMonthData
            .OrderBy(x => x.Key)
            .ThenBy(x => x.Value)
            .ToDictionary(x => x.Key, x => x.Value);
}