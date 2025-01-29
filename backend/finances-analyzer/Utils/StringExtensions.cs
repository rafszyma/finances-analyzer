namespace Utils;

public static class Utils
{
    public static string Sanitize(this string input)
    {
        return input.Replace("-", string.Empty).Replace(".", string.Empty).Replace(",", ".").Trim();
    }
}