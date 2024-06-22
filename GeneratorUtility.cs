namespace GodotInputMapExtension;

internal static class GeneratorUtility
{
    public static string Before(this string str, string delimeter)
    {
        var parts = str.Split(new[] { delimeter }, StringSplitOptions.None);
        if (parts.Length <= 1)
            return string.Empty;
        return parts[0];
    }

    public static string SnakeCaseToPascalCase(this string str) =>
        string.Concat(str.Split('_')
        .Select(Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase));
}
