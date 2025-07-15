using LanguageExt;
using LanguageExt.Common;

namespace Aggraze.Domain.Types;

public class Pips
{
    public decimal Value { get; }

    private Pips(decimal value)
    {
        this.Value = value;
    }

    public static Validation<Error, Pips> Create(decimal value)
    {
        if (value < 0)
        {
            return Error.New("Pips value must be greater than zero.");
        }

        return new Pips(value);
    }
}