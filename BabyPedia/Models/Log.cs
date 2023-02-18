namespace BabyPedia.Models;

public class Log
{
    public long Id { get; set; }

    public string Table { get; set; }

    public string TableColumn { get; set; }

    public string RowId { get; set; }

    public string OldValue { get; set; }

    public string NewValue { get; set; }

    public string UserId { get; set; }

    public string UpdateTime { get; set; }
}