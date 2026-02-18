using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using Shouldly;

namespace Aggraze.UnitTests.Calculators;

public class MaximumDrawdownCalculatorTests
{
    private readonly IMaximumDrawdownCalculator _calculator = new MaximumDrawdownCalculator();

    [Fact]
    public void Calculate_WhenGivenValidInput_ReturnsMaximumDrawdownOfGivenTrades()
    {
        // Arrange
        var builder = new TradeRowDataBuilder()
            .WithMaximumDrawdown(Pips.Create(12.5m))
            .Add()
            .WithMaximumDrawdown(Pips.Create(20))
            .Add()
            .Build();

        // Act
        var result = _calculator.Calculate(builder);

        // Assert
        result.ShouldBe(Pips.Create(20m));
    }
}