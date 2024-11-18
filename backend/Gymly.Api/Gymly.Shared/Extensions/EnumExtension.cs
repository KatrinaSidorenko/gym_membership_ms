namespace Gymly.Shared.Extensions;

public static class EnumExtension
{
    public static TEnum? ParseEnum<TEnum>(this string value) where TEnum : struct
    {
        if (Enum.TryParse<TEnum>(value, true, out var result))
        {
            return result;
        }

        return null;
    }
}
