using System.Globalization;
using Aggraze.Domain.Types;

namespace Aggraze.Application.Insights;

public class InsightBase
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    protected static IInsightResult CalculateInsight<T>(
        string name,
        IReadOnlyList<TradeRow> trades,
        Func<KeyValuePair<(int Year, int Month), IEnumerable<TradeRowData>>, T> calculateFunc,
        Func<IEnumerable<T>, T> summaryAggregator,
        SummaryType summaryType)
    {
        var groupedByYearAndMonth = GroupTradesByYearAndMonth(trades);

        var yearMonthData = new Dictionary<int, Dictionary<string, T>>();
        var summaryHelper = new Dictionary<string, List<T>>();
        
        foreach (var group in groupedByYearAndMonth)
        {
            AddYearMonthSummary(yearMonthData, summaryHelper, group.Key, calculateFunc(group));
        }

        var summary = new Summary<T>(
            summaryType,
            summaryHelper.ToDictionary(
                x => x.Key,
                x => summaryAggregator(x.Value)));

        var orderedYearMonthData = OrderYearMonthDataByYear(yearMonthData);

        return new InsightResult<T>(name, orderedYearMonthData, summary);
    }

    private static Dictionary<(int Year, int Month), IEnumerable<TradeRowData>> GroupTradesByYearAndMonth(IEnumerable<TradeRow> trades) =>
        trades
            .GroupBy(x => (x.Date.Year, x.Date.Month))
            .ToDictionary(
                group => group.Key,
                group => group.Select(y => y.Data));

    private static void AddYearMonthSummary<T>(
        Dictionary<int, Dictionary<string, T>> yearMonthData,
        Dictionary<string, List<T>> summaryHelper,
        (int Year, int Month) group,
        T yearMonthValue)
    {
        var year = group.Year;
        var month = Culture.DateTimeFormat.GetMonthName(group.Month);

        if (!yearMonthData.ContainsKey(year))
        {
            yearMonthData[year] = new Dictionary<string, T>();
        }

        yearMonthData[year][month] = yearMonthValue;

        if (!summaryHelper.ContainsKey(month))
        {
            summaryHelper[month] = [];
        }

        summaryHelper[month].AddRange(yearMonthValue);
    }

    private static Dictionary<int, Dictionary<string, T>> OrderYearMonthDataByYear<T>(Dictionary<int, Dictionary<string, T>> yearMonthData) =>
        yearMonthData
            .OrderBy(x => x.Key)
            .ThenBy(x => x.Value)
            .ToDictionary(x => x.Key, x => x.Value);
}