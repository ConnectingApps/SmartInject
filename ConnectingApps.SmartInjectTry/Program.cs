using ConnectingApps.SmartInject;
using ConnectingApps.SmartInjectTry.LazyClasses;

namespace ConnectingApps.SmartInjectTry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLazySingleton<ISomething, Something>();
            builder.Services.AddLazySingleton<ISomethingElse, SomethingElse>();

            builder.Services.AddLazyTransient<ISomethingA, SomethingA>();
            builder.Services.AddLazyTransient<ISomethingElseA, SomethingElseA>();

            builder.Services.AddLazyScoped<ISomethingB, SomethingB>();
            builder.Services.AddLazyScoped<ISomethingElseB, SomethingElseB>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}