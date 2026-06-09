

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddAuthentication("Bearer")
    ;
builder.Services.AddAuthorization();
builder.Services.AddControllers();
var app = builder.Build();
app.UseRouting();

// Add services to the container.

app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();


//app.MapControllers();
app.MapGet("/api/assessments/results", () => Results.Ok(new
{
courseCode = "CS-101",
studentId = "S-001",
letterGrade = "A"
})).RequireAuthorization();

app.Run();
