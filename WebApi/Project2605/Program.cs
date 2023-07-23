using BaseDataAccess;
using BaseRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<DbContextClass>();
builder.Services.AddDbContext<DbContextClass>(
options => options.UseSqlServer(
configuration.GetConnectionString("DefaultConnection"),
sqlServerOptionsAction: sqloptions => {
    sqloptions.EnableRetryOnFailure(
        maxRetryCount : 10,
        maxRetryDelay :TimeSpan.FromSeconds(5),
        errorNumbersToAdd:null
        );
}));
builder.Services.AddScoped<IapplicationUser, ApplicationUser>();
builder.Services.AddScoped<IEmployee, Employee>();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDeveloperExceptionPage();

app.UseStaticFiles();




app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
  
    endpoints.MapControllers(); //Routes for my API controllers
});


app.Run();
