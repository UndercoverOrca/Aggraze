using LanguageExt;
using LanguageExt.Common;

using static LanguageExt.Prelude;

namespace Aggraze.Domain.Types;

public readonly struct Pips
{
    public static readonly Pips Zero = new Pips(0);

    public static Pips operator +(Pips left, Pips right) => new(left.Value + right.Value);
    public static Pips operator -(Pips left, Pips right) => new(left.Value - right.Value);
    public static Pips operator *(Pips left, Pips right) => new(left.Value * right.Value);
    public static Pips operator /(Pips left, Pips right) => new(left.Value / right.Value);

    public decimal Value { get; }

    private Pips(decimal value)
    {
        this.Value = value;
    }
    
    public static Validation<Error, Pips> TryCreate(string value) =>
    IsValid(value)
        ? Success<Error, Pips>(new Pips(decimal.Parse(value)))
        : Fail<Error, Pips>("Pips value must be a valid decimal number greater than or equal to zero.");
    
    private static bool IsValid(string input) =>
        !string.IsNullOrWhiteSpace(input)
        && decimal.TryParse(input, out var result)
        && result >= 0;
}