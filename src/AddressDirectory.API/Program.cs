var builder = WebApplication.CreateBuilder(args);

// Add sevices to the container

// Application Services
builder.Services.AddCarter(null, config =>
{
    var modules = typeof(Program).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();
    config.WithModules(modules);
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddLogging();

// Data Services
builder.Services.AddDbContext<AddressContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<IAddressRepository, AddressRepository>();

var app = builder.Build();

app.MapCarter();

app.UseMigrations();

app.Run();
