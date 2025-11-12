using DevWorkTracker.Api.Models;

namespace DevWorkTracker.Api.Services;

public interface IIssueService
{
    //create ienumerable for easy iteration of issues
    IEnumerable<Issue> GetAll();
    Issue Create(string title, string? description);
    Issue UpdateStatus(int id, IssueStatus status);
}