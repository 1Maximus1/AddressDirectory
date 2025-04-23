namespace AddressDirectory.API.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<AddressContext>();
            dbContext.Database.Migrate();

            return app;
        }
    }
}
