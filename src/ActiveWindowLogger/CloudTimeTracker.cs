using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace ActiveWindowLogger;

public class CloudTimeTracker
{
    private string URL { get; set; }
    private string User { get; set; }
    private string Secret { get; set; }
    private readonly TimeSpan MinimumTimeBetweenLogs = TimeSpan.FromMinutes(1);
    private DateTime LastRequestUtc = DateTime.MinValue;

    HttpClient HttpClient { get; } = new();

    public CloudTimeTracker()
    {
        string cloudSecretFile = Path.GetFullPath("CloudSecret.txt");
        if (!File.Exists(cloudSecretFile))
            File.WriteAllText(cloudSecretFile, "https://...\nuser\n<secret>");

        string[] lines = File.ReadAllText(cloudSecretFile).Split("\n");
        URL = lines[0];
        User = lines[1];
        Secret = lines[2];

        if (Secret.StartsWith('<') || User.StartsWith('<') || URL.Contains("..."))
            throw new KeyNotFoundException("Cloud secret file must be populated. " +
                "Set first line to localhost to disable cloud tracking. " +
                cloudSecretFile);
    }

    public async Task LogActivity()
    {
        TimeSpan timeSinceLastLog = DateTime.UtcNow - LastRequestUtc;
        if (timeSinceLastLog < MinimumTimeBetweenLogs)
            return;

        LastRequestUtc = DateTime.UtcNow;
        string timestamp = DateTime.UtcNow.ToString("O");
        string hash = GetHash(User, timestamp, Secret);
        AddEventBody body = new(User, timestamp, hash);
        await HttpClient.PostAsJsonAsync(URL, body);
    }

    public record AddEventBody(string User, string Timestamp, string Hash);

    public static string GetHash(string user, string metric, string secret)
    {
        string input = $"{user}.{metric}.{secret}";
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = MD5.HashData(inputBytes);
        return Convert.ToHexString(hashBytes);
    }
}