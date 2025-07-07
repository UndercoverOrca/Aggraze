using LanguageExt;
using static LanguageExt.Prelude;

namespace Aggraze.Domain;

public static class DictionaryExtensions
{
    public static Option<T> TryGetValue<T>(this Dictionary<string, string> dictionary, string key) =>
        dictionary.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value)
            ? Some((T)Convert.ChangeType(value, typeof(T)))
            : None;
}