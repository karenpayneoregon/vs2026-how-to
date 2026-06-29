namespace EF_JsonHybridSample.Models;

public class MailSettings
{
    public string? FromAddress { get; set; }

    public string? Host { get; set; }

    public string? PickupFolder { get; set; }

    public int Port { get; set; }

    public int TimeOut { get; set; }
}