using LanguageExt;
using static LanguageExt.Prelude;

namespace Aggraze.Domain.Extensions;

public static class LinqExtensions
{
    public static Option<TResult> MapIfSome<T, TResult>(this Option<T> option, Func<T, TResult> func) =>
        option.Match(
            x => Some(func(x)),
            () => None
        );
}