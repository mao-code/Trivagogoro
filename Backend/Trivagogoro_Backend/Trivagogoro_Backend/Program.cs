var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region CORS
var LocalPolicy = "local";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: LocalPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
        });
});
#endregion

#region Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
#endregion

var app = builder.Build();

// use local CORS policy
app.UseCors(LocalPolicy);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

