using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Logica;
using CH_BACKEND.Repositories; // Asegúrate de importar el repositorio

var builder = WebApplication.CreateBuilder(args);

// 🔹 Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Registrar DbContext (si usas Entity Framework)
builder.Services.AddDbContext<_DbContextCalzadosHuancayo>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=LAPTOP-6HLGI3JI\\SQLEXPRESS;Database=Calzados;Trusted_Connection=True;TrustServerCertificate=True;")));


builder.Services.AddScoped<VentaRepositorio>(); // Registrar el repositorio primero
builder.Services.AddScoped<VentaLogica>(); // Luego registrar la lógica de ventas
builder.Services.AddScoped<CategoriaRepositorio>(); // Registrar el repositorio primero
builder.Services.AddScoped<CategoriaLogica>(); // Luego registrar la lógica de ventas
builder.Services.AddScoped<DevolucionRepository>(); // Registrar el repositorio primero
builder.Services.AddScoped<DevolucionLogica>(); // Registrar la lógica de devoluciones
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


var app = builder.Build();

// 🔹 Configurar el pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
