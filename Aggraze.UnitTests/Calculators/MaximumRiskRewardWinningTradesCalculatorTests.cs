using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using Shouldly;
using Xunit;

namespace Aggraze.UnitTests.Calculators;

public class MaximumRiskRewardWinningTradesCalculatorTests
{
    private readonly IMaximumRiskRewardWinningTradesCalculator _calculator = new MaximumRiskRewardWinningTradesCalculator();

    [Fact]
    public void Calculate_WhenGivenValidInput_ReturnsTheLowestMaximumResultOfAllWonTrades()
    {
        // Arrange
        var builder = new TradeRowDataBuilder()
            .WithResult(Result.Win)
            .WithMaximumResult(20m)
            .Add()
            .WithResult(Result.Win)
            .WithMaximumResult(50m)
            .WithResult(Result.Loss)
            .WithMaximumResult(10m)
            .Add()
            .Build();

        // Act
        var result = _calculator.Calculate(builder);

        // Assert
        result.ShouldBe(20m);
    }
}