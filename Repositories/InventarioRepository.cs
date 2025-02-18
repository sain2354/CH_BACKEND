using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.Repositories
{
    public class InventarioRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public InventarioRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public List<Inventario> ObtenerTodos()
        {
            return _context.Inventarios.Include(i => i.IdProductoNavigation).ToList();
        }

        public Inventario? ObtenerPorId(int id)
        {
            return _context.Inventarios.Include(i => i.IdProductoNavigation).FirstOrDefault(i => i.IdInventario == id);
        }

        public List<Inventario> FiltrarPorProductoYFecha(int idProducto, DateOnly fecha)
        {
            return _context.Inventarios
                .Where(i => i.IdProducto == idProducto && i.FechaRegistro == fecha)
                .Include(i => i.IdProductoNavigation)
                .ToList();
        }

        public void Agregar(Inventario inventario)
        {
            _context.Inventarios.Add(inventario);
            _context.SaveChanges();
        }
    }
}