using DevWorkTracker.Api.Services;
using DevWorkTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//singleton==> we only want one instance of each per application run
builder.Services.AddSingleton<IWorkSessionService, WorkSessionService>();
builder.Services.AddSingleton<IIssueService, IssueService>();
builder.Services.AddSingleton<IProjectRepository, ProjectRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//mapping endpoint        // this second arg is the lambda (delegate)
app.MapGet("/sessions", (IWorkSessionService service) =>
{
    // give the client 200 HTTP indicating successful response, session returned as body in JSON
    return Results.Ok(service.GetAll());
});

app.MapPost("/sessions/start", (IWorkSessionService service, SessionStartRequest request) =>
{
    var session = service.StartSession(request.ProjectName, request.Language, request.Description);
    return Results.Created($"/sessions/{session.Id}", session); //
});

app.MapPost("/sessions/{id:int}/stop", (IWorkSessionService service, int id) =>
{
    var session = service.StopSession(id);
    return Results.Ok(session);
});

app.MapGet("/sessions/summary/projects", (IWorkSessionService service) =>
{
    return Results.Ok(service.GetAllProjectsAndDuration());
});

app.MapGet("/sessions/summary/languages", (IWorkSessionService service) =>
{
    return Results.Ok(service.GetDurationByLanguage());
});

app.MapGet("/issues", (IIssueService service) =>
{
    return Results.Ok(service.GetAll());
});

app.MapPost("/issues", (IIssueService service, IssueCreateRequest request) =>
{
    var issue = service.Create(request.Title, request.Description);
    return Results.Created($"/issues/{issue.Id}", issue);
});

app.MapPatch("/issues/{id:int}/status", (IIssueService service, int id, IssueStatusUpdateRequest request) =>
{
    var issue = service.UpdateStatus(id, request.Status);
    return Results.Ok(issue);
});

app.MapGet("/projects", (IProjectRepository service) =>
{
    service.TestFilePath();
    return Results.Ok(service.GetAll());
});
app.MapPost("project create", (IProjectRepository service, string name, string desc) =>
{
    var proj = service.CreateNew(name, desc);
    return Results.Ok(proj);
});

//RUN ON STARTUP
using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Chicken Micken");

    var repo = scope.ServiceProvider.GetRequiredService<IProjectRepository>();

    //make sure file path is accurate
    repo.TestFilePath();
    repo.SortProjects();
}
// using block is for returning IDisposable; scope.Dispose()

app.Run();