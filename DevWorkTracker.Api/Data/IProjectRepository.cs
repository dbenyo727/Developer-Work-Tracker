namespace DevWorkTracker.Api.Models;

public interface IProjectRepository
{
    Project? GetById(int id);
    List<Project> GetAll();

    Project CreateNew(string name, string desc);
    void TestFilePath();
    int GetNextProjectID();
    void SortProjects();
    
}