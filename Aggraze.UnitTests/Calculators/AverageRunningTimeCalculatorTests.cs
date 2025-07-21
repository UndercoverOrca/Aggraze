using Aggraze.Domain.Calculators;
using Shouldly;

namespace Aggraze.UnitTests.Calculators;

public class AverageRunningTimeCalculatorTests
{
    private readonly IAverageRunningTimeCalculator _calculator = new AverageRunningTimeCalculator();

    [Fact]
    public void Calculate_WhenGivenValidInput_ReturnsAverageRunningTime()
    {
        // Arrange
        var builder = new TradeRowDataBuilder()
            .WithOpenTime(new TimeOnly(10, 00, 00))
            .WithClosingTime(new TimeOnly(10, 30, 0))
            .Add()
            .WithOpenTime(new TimeOnly(13, 00, 0))
            .WithClosingTime(new TimeOnly(15, 00, 0))
            .Add()
            .Build();

        // Act
        var result = _calculator.Calculate(builder);

        // Assert
        result.ShouldBe(TimeSpan.FromMinutes(75));
    }
}