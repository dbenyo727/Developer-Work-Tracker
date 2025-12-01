using DevWorkTracker.Api.Models;

// this is where most of our unit tests will target
namespace DevWorkTracker.Api.Services;

public class WorkSessionService : IWorkSessionService
{
    private readonly List<WorkSession> _sessions = new();
    private int _nextId = 1;

    public IEnumerable<WorkSession> GetAll() => _sessions;

    public WorkSession StartSession(string projectName, string language, string? description)
    {
        if (string.IsNullOrWhiteSpace(projectName))
            throw new ArgumentException("Project name musn't be null", nameof(projectName));

        if (string.IsNullOrWhiteSpace(language))
            throw new ArgumentException("Language is required", nameof(language));

        var session = new WorkSession
        {
            Id = _nextId++,
            ProjectName = projectName,
            Language = language,
            SessionDesc = description,
            StartedAt = DateTime.UtcNow
        };

        _sessions.Add(session);
        return session;
    }

    public WorkSession StopSession(int id)
    {                                          //key found when a session matches ID, execption othrwise
        var session = _sessions.SingleOrDefault(s => (s.Id == id))
            ?? throw new KeyNotFoundException($"Session {id} not found");

        // project is not still in progress
        if (session.EndedAt != null)
            throw new InvalidOperationException($"Session {id} already stopped");

        session.EndedAt = DateTime.UtcNow;
        session.DurationMinutes = (session.EndedAt.Value - session.StartedAt).TotalMinutes;
        return session;
    }
    public IEnumerable<IDictionary<string, double>> GetAllProjectsAndDuration()
    {
        var ReturnList = new List<IDictionary<string, double>>();

        foreach (WorkSession w in _sessions)
        {
            if (ReturnList.Count == 0)
            {
                ReturnList.Add(new Dictionary<string, double> { { w.ProjectName, w.DurationMinutes } });
            }
            else
            {
                foreach (IDictionary<string, double> i in ReturnList)
                {
                    if (i.ContainsKey(w.ProjectName))
                    {
                        i[w.ProjectName] += w.DurationMinutes;
                        break;
                    }
                    else
                    {
                        ReturnList.Add(new Dictionary<string, double> { { w.ProjectName, w.DurationMinutes } });
                        break;
                    }
                }
            }
        }
        return ReturnList;
    }
    //Identifier is project name, shows total minutes for that specific project
    public IDictionary<string, double> GetDurationByProject()
    {
        var returner = new Dictionary<string, double>();

        foreach (WorkSession s in _sessions)
        {
            if (returner.ContainsKey(s.ProjectName))
            {
                returner[s.Language] += s.DurationMinutes;
            }
            else
            {
                //no project with specified name has been iterated thru
                returner[s.Language] = s.DurationMinutes;
            }
        }
        return returner;
    }
    
    //Finds all projects with a specified language and sums their independent total times
    public IDictionary<string, double> GetDurationByLanguage()
    {
        var result = new Dictionary<string, double>();

        foreach (var session in _sessions)
        {
             var language = session.Language;
             var duration = session.DurationMinutes;

            if (result.ContainsKey(language))
            {
                result[language] += duration;
            }
             else
            {
                result[language] = duration;
            }
        }

        return result;
    }

    public IDictionary<string, double> GetProjectByID()
    {
        return null;
    }
}
