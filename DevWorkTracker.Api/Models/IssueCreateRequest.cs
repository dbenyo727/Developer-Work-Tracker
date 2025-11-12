namespace DevWorkTracker.Api.Models;

public class IssueCreateRequest
{
    public string Title { get; set; } = "HTTP request error";
    public string Description { get; set; } = "Failed to connect to ......";
}
public class IssueStatusUpdateRequest
{
    public IssueStatus Status { get; set; } = IssueStatus.Open;
}