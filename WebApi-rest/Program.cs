
using ActividadN1.Data;
using ActividadN1.Data.IRepository;
using ActividadN1.Service.IService;
using ActividadN1.Service;
using Microsoft.Data.SqlClient;

namespace WebApi_rest
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


            builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            builder.Services.AddScoped<Func<SqlTransaction, IInvoiceRepository>>(sp =>
            tx => new InvoiceRepository(tx));
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
