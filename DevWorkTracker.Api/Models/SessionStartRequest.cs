namespace DevWorkTracker.Api.Models;

public class SessionStartRequest
{
    public string ProjectName { get; set; } = "New Project";
    public int ProjectID { get; set; }
    public string Language { get; set; } = "Python";
    public string? Description { get; set; }
}