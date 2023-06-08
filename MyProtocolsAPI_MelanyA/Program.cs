using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyProtocolsAPI_MelanyA.Models;

namespace MyProtocolsAPI_MelanyA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Vamos a leer la etiqueta de CNNSTR de appsettings. json para configurar la conexion 
            // a la base de datos 

            var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));

            //Eliminamos de CNNSTR el dato del password ya que seria muy sencillo obtener la 
            //info de conexion del usuario de SQL Server del archivo de config appsettings.json

            CnnStrBuilder.Password = "123456";

            //CnnStrBuilder es un objeto que permite la construccion de cadenas de conexion a bases
            // de datos. Se pueden modificar cada parte de la misma, pero al final debemos extraer un string
            // con la info final.

            string cnnStr = CnnStrBuilder.ConnectionString;

            // ahora conectamos el proyecto a la base de datos usando el cnnStr
            builder.Services.AddDbContext<MyProtocolsDBContext>(options => options.UseSqlServer(cnnStr));





            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}