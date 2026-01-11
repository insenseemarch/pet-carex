using System.Globalization;
using System.Text;

namespace StaffWeb.Utils;

public static class TextUtil
{
    public static string Normalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "";
        }

        var normalized = input.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(normalized.Length);

        foreach (var ch in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(ch);
            }
        }

        var cleaned = sb.ToString()
            .Replace('đ', 'd')
            .Replace('Đ', 'D');

        return cleaned.Normalize(NormalizationForm.FormC).ToLowerInvariant();
    }
}
