using CategoryApi.Data;
using CategoryApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application services
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

/*
  CORS POLICY (ad: "MvcClient")
  Bu ad sadəcə "etiket"dir. İstəsən "AllowMvc" də qoya bilərsən.
  Məqsəd: hansı qaydanı çağıracağımızı adla seçmək.
*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("MvcClient", policy =>
    {
        /*
          WithOrigins(...) = hansı "origin"-lər (sayt/port) icazəlidir.
          Bu origin MVC-nin BRAUZERDƏ açıldığı URL-dir.
        */
        policy.WithOrigins("https://localhost:7266")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

/*
  Bu sətir yuxarıda ad verdiyimiz policy-ni işə salır.
  "MvcClient" = policy adı (özün qoymusan).
*/
app.UseCors("MvcClient");

app.MapControllers();

app.Run();
