using System.Text.Json;

namespace KhachHangWeb.Extensions;

public static class SessionExtensions
{
    private static readonly JsonSerializerOptions Options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    public static void SetObject<T>(this ISession session, string key, T value) =>
        session.SetString(key, JsonSerializer.Serialize(value, Options));
    public static T? GetObject<T>(this ISession session, string key) =>
        session.GetString(key) is { } json ? JsonSerializer.Deserialize<T>(json, Options) : default;
}
