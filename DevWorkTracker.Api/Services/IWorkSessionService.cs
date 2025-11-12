using DevWorkTracker.Api.Models;

namespace DevWorkTracker.Api.Services;

public interface IWorkSessionService
{
    IEnumerable<WorkSession> GetAll();
    WorkSession StartSession(string projectName, string language, string? description);
    WorkSession StopSession(int id);
    IDictionary<string, double> GetDurationByProject();
    IDictionary<string, double> GetDurationByLanguage();
}
