using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Logica;
using CH_BACKEND.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1) Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2) Configurar CORS (Permitir cualquier origen)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// 3) Registrar DbContext (verifica que la cadena sea correcta)
builder.Services.AddDbContext<_DbContextCalzadosHuancayo>(options =>
    options.UseSqlServer(
        "workstation id=Calzados.mssql.somee.com;packet size=4096;user id=pepe12_SQLLogin_2;pwd=zhl9uctlun;data source=Calzados.mssql.somee.com;persist security info=False;initial catalog=Calzados;TrustServerCertificate=True"
    )
);

// 4) Registrar servicios y lógica
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
builder.Services.AddScoped<VentaRepositorio>();
builder.Services.AddScoped<VentaLogica>();
builder.Services.AddScoped<CarritoRepositorio>();
builder.Services.AddScoped<CarritoLogica>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioLogica>();
builder.Services.AddScoped<UsuarioDireccionRepository>();
builder.Services.AddScoped<UsuarioDireccionLogica>();
builder.Services.AddScoped<DireccionEntregaRepository>();
builder.Services.AddScoped<DireccionEntregaLogica>();

var app = builder.Build();

// 5) Habilitar servir archivos estáticos desde wwwroot (si Somee lo permite)
app.UseStaticFiles();

// 6) Swagger
app.UseSwagger();
app.UseSwaggerUI();

// 7) (Opcional) Redirección HTTPS. Si Somee no soporta HTTPS, coméntalo.
// app.UseHttpsRedirection();

// 8) CORS
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();
