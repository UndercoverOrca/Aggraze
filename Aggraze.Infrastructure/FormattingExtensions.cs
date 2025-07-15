namespace Aggraze.Infrastructure;

public static class FormattingExtensions
{
    public static string ToFormattedString(this object value) =>
        (value switch
        {
            TimeSpan ts => ts.ToString(@"h\:mm\:ss"),
            DateTime dt => dt.ToString("g"),
            decimal d => d.ToString("N2"),
            // Pips p => p.ToString(),
            _ => value.ToString()
        })!;
}