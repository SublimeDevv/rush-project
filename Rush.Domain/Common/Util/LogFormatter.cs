using System.Text.Json;

public static class LogFormatter
{
    private static readonly Dictionary<int, string> HttpMethods = new()
    {
        {0, "GET"}, {1, "POST"}, {2, "PUT"}, {3, "DELETE"}
    };

    public static string FormatAsChatMessage(string jsonLog)
    {
        var log = JsonSerializer.Deserialize<LogEntry>(jsonLog);

        return $@"
📅 *{log.TimeStamp:yyyy-MM-dd HH:mm:ss}* | {GetIcon(log.Level)}
🔧 *Endpoint:* {log.Endpoint} ({HttpMethods[log.HttpMethod]})
👤 *Usuario:* {(log.UserId?[..8] + "..." ?? "Anonymous")}
📝 *Mensaje:* {log.Message}
🆔 *ID:* {log.Id[..8]}...
        ".Trim();
    }

    private static string GetIcon(int level) => level switch
    {
        1 => "🔥",   // Error
        2 => "⚠️",   // Warning
        3 => "ℹ️",   // Info
        4 => "✅",   // Success
        _ => "🔍"    // Default
    };
}

public class LogEntry
{
    public string Message { get; set; }
    public int HttpMethod { get; set; }
    public string Endpoint { get; set; }
    public int Level { get; set; }
    public string UserId { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
}