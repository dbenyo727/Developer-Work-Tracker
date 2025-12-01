namespace DevWorkTracker.Api.Models;

public class Project
{
    public string Name { get; set; } = "New Project";
    public int ID { get; set; } = -1;
    public string Description { get; set; } = "This is a newly created project. Please edit JSON.";
    public double TotalDuration { get; set; }
    public DateTime StartDate {get; set;}
}