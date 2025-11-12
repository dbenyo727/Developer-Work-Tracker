namespace DevWorkTracker.Api.Models;

public class Issue
{
    public int Id {get; set;}
    public string Title {get; set;} = "";
    public string? Description { get; set; }
    // current status
    public IssueStatus Status {get; set;} = IssueStatus.Open;

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public DateTime? ClosedAt {get; set;}
}