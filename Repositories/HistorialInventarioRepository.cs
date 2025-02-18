using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.Repositories
{
    public class HistorialInventarioRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public HistorialInventarioRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public List<HistorialInventario> ObtenerTodos()
        {
            return _context.HistorialInventarios.Include(h => h.IdProductoNavigation).ToList();
        }

        public HistorialInventario? ObtenerPorId(int id)
        {
            return _context.HistorialInventarios.Include(h => h.IdProductoNavigation).FirstOrDefault(h => h.IdMovimiento == id);
        }

        public void Agregar(HistorialInventario historial)
        {
            _context.HistorialInventarios.Add(historial);
            _context.SaveChanges();
        }
    }
}