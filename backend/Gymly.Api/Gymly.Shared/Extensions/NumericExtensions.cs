namespace Gymly.Shared.Extensions;

public static class NumericExtensions
{
    public static long? GetLongOrNull(this string value)
    {
        if (long.TryParse(value, out var result))
        {
            return result;
        }

        return null;
    }
}
