using DotnetAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((options) =>
{
    options.AddPolicy("DevCors", (corsBulder) =>
    {
        corsBulder.WithOrigins("http://localhost:4200", "http://127.0.0.1:5174", "http://127.0.0.1:5173")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
        
    }); 
    options.AddPolicy("ProdCors", (corsBulder) =>
    {
    
        corsBulder.WithOrigins("http://myproductionSite.com", "http://127.0.0.1:5174", "http://127.0.0.1:5173", "https://master--statuesque-toffee-c8c15f.netlify.app", "https://statuesque-toffee-c8c15f.netlify.app/")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();

    });
}
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, Studentrepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IClassRoomRepository,ClassRoomRepository>();
builder.Services.AddScoped<IAdllocateSubjectRepository, AllocateSubjectRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("ProdCors");
    app.UseHttpsRedirection();
}

//
app.UseCors("AllowAnyOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
