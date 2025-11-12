using System.Net;
using DevWorkTracker.Api.Models;

namespace DevWorkTracker.Api.Services;

                        // this is the interface, anything of IIssueservice most have the following properties.
                        //class issueservice must implement interface. this class must provide all methods from iissueservice
public class IssueService : IIssueService
{
    private readonly List<Issue> _issues = new();
    private int _nextId = 1;

    //public return service for private issueservive list
    public IEnumerable<Issue> GetAll() => _issues;

    public Issue Create(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
            //more on nameof and throw****
            throw new ArgumentException("title required", nameof(title));

        var issue = new Issue
        {
            Id = _nextId++,
            Title = title,
            Description = description,
            Status = IssueStatus.Open
        };

        _issues.Add(issue);
        return issue;
    }
    public Issue UpdateStatus(int id, IssueStatus status)
    {
        //pls more on singleordefault, and ??
        var issue = _issues.SingleOrDefault(i => i.Id == id)
            ?? throw new KeyNotFoundException($"Issue {id} not found");

        if (issue.Status == status)
            return issue;

        if (status == IssueStatus.Closed)
            issue.ClosedAt = DateTime.UtcNow;
        else
            issue.ClosedAt = null;

        return issue;
    }
}