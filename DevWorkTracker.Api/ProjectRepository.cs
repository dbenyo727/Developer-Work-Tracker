using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
namespace DevWorkTracker.Api.Models;

public class ProjectRepository : IProjectRepository
{
    private readonly string _filePath = "ProjectList.json";
    int nextID = 1;

    public ProjectRepository()
    {
        //dig into the root-->bin-->debug-->net8.0-->Data-->[fileName]
        _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "ProjectList.json");
    }
    public List<Project> LoadProjects()
    {
        if (!File.Exists(_filePath))
            throw new Exception("File not found");

        var JSON = File.ReadAllText(_filePath);
        var Projects = JsonSerializer.Deserialize<List<Project>>(JSON);

        return Projects;
    }
    public void TestFilePath()
    {
        Console.WriteLine(AppContext.BaseDirectory);
    }

    public Project GetById(int id)
    {
        var JSON = File.ReadAllText(_filePath);
        var Projects = JsonSerializer.Deserialize<List<Project>>(JSON);

        return Projects.FirstOrDefault(p => p.ID == id);
    }

    public List<Project> GetAll()
    {
        var JSON = File.ReadAllText(_filePath);
        var Projects = JsonSerializer.Deserialize<List<Project>>(JSON);

        return Projects;
    }
    public Project CreateNew(string name, string desc)
    {
        var newProj = new Project();
        newProj.Name = name;
        newProj.ID = nextID;
        newProj.Description = desc;

        newProj.StartDate = DateTime.UtcNow;

        var p = GetAll();

        p.Add(newProj);
        Console.WriteLine(_filePath);

        SaveProjects(p);
        

        return newProj;
    }
    private void SaveProjects(List<Project> projects)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(projects, options);

        File.WriteAllText(_filePath, json);
    }
    public int GetNextProjectID()
    {
        //obtain list of all projects and assuming their ids match
        //their value within the list, the next ID should be length of list
        return GetAll().Count;
    }
    public void SortProjects()
    {
        Console.WriteLine("Sorting Projects....");
        var proj = GetAll().OrderBy(pjct => 
        {
            Console.WriteLine($"{pjct.Name} --- {pjct.ID}");
            return pjct.ID;
        })
        //problems without this part; lazy query does not auto return list
        .ToList();

        SaveProjects(proj);

    }
}