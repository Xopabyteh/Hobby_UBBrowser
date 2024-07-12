using Hobby_UBBrowser.API;
using Hobby_UBBrowser.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<BrowserService>();

var app = builder.Build();

app.Services.GetService<BrowserService>()!.Init();

app.RegisterEndpoints();
app.Run();