namespace DevWorkTracker.Api.Models;

public class WorkSession
{
    public int Id {get; set;}
    public string ProjectName {get; set;} = "";
    public string Language {get; set;} = "";
    public string? SessionDesc { get; set; }
    public string Notes { get; set; } = "nothing to see here";

    public DateTime StartedAt {get; set;}
    public DateTime? EndedAt {get; set;}

    // Cache of total duration in minutes for quick summary
    public double DurationMinutes { get; set; }
}