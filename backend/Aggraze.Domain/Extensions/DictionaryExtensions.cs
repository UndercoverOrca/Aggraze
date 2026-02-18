using LanguageExt;
using static LanguageExt.Prelude;

namespace Aggraze.Domain.Extensions;

public static class DictionaryExtensions
{
    public static Option<string> GetValueOrNone(this Dictionary<string, string> data, string key) =>
        data.TryGetValue(key, out var value)
        && !IsEmptyString(value)
            ? Some(value) 
            : None;
    
    private static bool IsEmptyString(string value) =>
        string.IsNullOrWhiteSpace(value)
        || value == "-"
        || value == "--"
        || value == "N/A"
        || value == "N/A"
        || value == "NVT";
}