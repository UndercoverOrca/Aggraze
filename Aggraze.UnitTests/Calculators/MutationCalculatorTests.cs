using Aggraze.Domain.Calculators;
using Shouldly;
using Xunit;

namespace Aggraze.UnitTests.Calculators;

public class MutationCalculatorTests
{
    private readonly IMutationCalculator _calculator = new MutationCalculator();

    [Fact]
    public void Calculate_WhenGivenValidInput_ReturnsPositiveMutation()
    {
        // Arrange
        var builder = new TradeRowDataBuilder()
            .WithMutation(1)
            .Add()
            .WithMutation(1)
            .Add()
            .Build();

        // Act
        var result = _calculator.Calculate(builder);

        // Assert
        result.ShouldBe(2);
    }
}