using azure_test_api.Data;


var builder = WebApplication.CreateBuilder(args);
bool alwaysSwagger = true; // Allow swagger testing in prod Azure

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add all new APIs here
builder.Services.AddScoped<IMyItemRepo, MyItemRepo>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (alwaysSwagger || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/version", () =>
{
    return "1.0.1";
})
.WithName("TestAPIVersion")
.WithOpenApi();


#region API SQL testing
var SQLTesting = app.MapGroup("sqltests");
SQLTesting.MapGet("GetItems", async (IMyItemRepo repoItems) =>
{
    return Results.Ok(await repoItems.GetItems());
});
#endregion

app.Run();

