using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using LanguageExt;

using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the gain/loss of the of a traders' taken (backtest) trades, expressed in %.
/// </summary>
public class Mutation : IInsight
{
    public string Name => "Mutation";

    private readonly IMutationCalculator _mutationCalculator;

    public Mutation(IMutationCalculator mutationCalculator) =>
        _mutationCalculator = mutationCalculator;

    public Option<IInsightResult> GenerateInsight(IReadOnlyList<TradeRow> trades) =>
        trades
            .Any(ContainsRequiredHeaders)
        ? Some(this._mutationCalculator.Calculate(Name, trades.Where(ContainsRequiredHeaders).ToList()))
        : None;
    
    private static Func<TradeRow, bool> ContainsRequiredHeaders => x =>
        x.Data.Date.IsSome
        && x.Data.Mutation.IsSome
        && x.Data.Result.IsSome;
}