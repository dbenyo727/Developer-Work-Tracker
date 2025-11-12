namespace DevWorkTracker.Api.Models;

public class WorkSession
{
    public int Id {get; set;}
    public string ProjectName {get; set;} = "";
    public string Language {get; set;} = "";
    public string? Description {get; set;}

    public DateTime StartedAt {get; set;}
    public DateTime? EndedAt {get; set;}

    // Cache of total duration in minutes for quick summary
    public double DurationMinutes { get; set; }
}