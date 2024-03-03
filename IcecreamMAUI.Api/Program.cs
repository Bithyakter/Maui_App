using IcecreamMAUI.Api.Data;
using IcecreamMAUI.Api.Endpoints;
using IcecreamMAUI.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString = builder.Configuration.GetConnectionString("DBLocation");
//builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Icecream")));

builder.Services.AddAuthentication(options =>
{
   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(jwtOptions =>
   {
      jwtOptions.TokenValidationParameters = TokenService.GetTokenValidationParameters(builder.Configuration);
   });

builder.Services.AddAuthentication();
builder.Services.AddTransient<TokenService>()
                .AddTransient<PasswordService>()
                .AddTransient<AuthService>();
//builder.Services.AddTransient<PasswordService>();
builder.Services.AddAuthentication();

var app = builder.Build();

#if DEBUG
MigrateDatabase(app.Services);
#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();

//app.MapControllers();

app.MapEndpoints();

app.Run();

static void MigrateDatabase(IServiceProvider sp)
{
   var scope = sp.CreateScope();
   var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
   if (dataContext.Database.GetPendingMigrations().Any())
      dataContext.Database.Migrate();
}
