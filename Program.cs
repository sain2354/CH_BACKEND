using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Logica;
using CH_BACKEND.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1) Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2) Configurar CORS (Permitir cualquier origen en dev)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// 3) Registrar DbContext
builder.Services.AddDbContext<_DbContextCalzadosHuancayo>(options =>
    options.UseSqlServer(
        "workstation id=Calzados.mssql.somee.com;packet size=4096;user id=pepe12_SQLLogin_2;pwd=zhl9uctlun;data source=Calzados.mssql.somee.com;persist security info=False;initial catalog=Calzados;TrustServerCertificate=True"
    )
);


/// 4) Registrar servicios y lógica (tal como lo tenías)
builder.Services.AddScoped<VentaRepositorio>();
builder.Services.AddScoped<VentaLogica>();
builder.Services.AddScoped<CategoriaRepositorio>();
builder.Services.AddScoped<CategoriaLogica>();
builder.Services.AddScoped<DevolucionRepository>();
builder.Services.AddScoped<DevolucionLogica>();
builder.Services.AddScoped<DetalleVentaRepository>();
builder.Services.AddScoped<EmpresaLogica>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<HistorialInventarioRepository>();
builder.Services.AddScoped<HistorialInventarioLogica>();
builder.Services.AddScoped<InventarioRepository>();
builder.Services.AddScoped<InventarioLogica>();
builder.Services.AddScoped<MedioPagoRepository>();
builder.Services.AddScoped<MedioPagoLogica>();
builder.Services.AddScoped<PagoRepository>();
builder.Services.AddScoped<PagoLogica>();
builder.Services.AddScoped<PersonaRepositorio>();
builder.Services.AddScoped<PersonaLogica>();
builder.Services.AddScoped<ProductoRepositorio>();
builder.Services.AddScoped<ProductoLogica>();
builder.Services.AddScoped<PromocionRepository>();
builder.Services.AddScoped<PromocionLogica>();
builder.Services.AddScoped<RolRepository>();
builder.Services.AddScoped<RolLogica>();
builder.Services.AddScoped<SubCategoriaRepository>();
builder.Services.AddScoped<SubCategoriaLogica>();
builder.Services.AddScoped<TallaRepository>();
builder.Services.AddScoped<TallaLogica>();
builder.Services.AddScoped<TallaProductoRepository>();
builder.Services.AddScoped<TallaProductoLogica>();
builder.Services.AddScoped<UnidadMedidaRepository>();
builder.Services.AddScoped<UnidadMedidaLogica>();
builder.Services.AddScoped<UsuarioRolRepositorio>();
builder.Services.AddScoped<UsuarioRolLogica>();
builder.Services.AddScoped<CarritoRepositorio>();
builder.Services.AddScoped<CarritoLogica>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioLogica>();
builder.Services.AddScoped<UsuarioDireccionRepository>();
builder.Services.AddScoped<UsuarioDireccionLogica>();
builder.Services.AddScoped<DireccionEntregaRepository>();
builder.Services.AddScoped<DireccionEntregaLogica>();


var app = builder.Build();

// 5) Developer Exception Page para ver errores de arranque
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 6) Servir archivos estáticos de wwwroot (incluye css, js, etc.)
app.UseStaticFiles();

// 7) Servir imágenes desde wwwroot/images
var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
if (Directory.Exists(imagesPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(imagesPath),
        RequestPath = "/images"
    });
}

// 7.1) Servir archivos estáticos de wwwroot/uploads
var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
if (Directory.Exists(uploadsPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(uploadsPath),
        RequestPath = "/uploads"
    });
}

// 8) Enrutamiento
app.UseRouting();

// 9) Middleware global de excepción para exponer InnerException
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        if (feature?.Error != null)
        {
            var ex = feature.Error;
            while (ex.InnerException != null)
                ex = ex.InnerException;
            var detalle = ex.Message;

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync($"Error interno (detalle): {detalle}");
        }
    });
});

// 10) Aplicar CORS
app.UseCors("AllowAll");

// 11) Autenticación/Autorización (si aplica)
app.UseAuthorization();

// 12) Mapear controladores
app.MapControllers();

app.Run();

















