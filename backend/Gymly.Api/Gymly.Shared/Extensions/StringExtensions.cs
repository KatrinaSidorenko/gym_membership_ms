namespace Gymly.Shared.Extensions;

public static class StringExtensions
{
    public static string Append(this string str, string value, string separator = " ")
        => string.IsNullOrWhiteSpace(str) ? value : $"{str}{separator}{value}";
}
