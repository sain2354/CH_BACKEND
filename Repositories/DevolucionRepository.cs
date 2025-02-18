using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.Repositories
{
    public class DevolucionRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public DevolucionRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public List<Devolucion> ObtenerTodas()
        {
            return _context.Devoluciones.ToList();
        }

        public Devolucion? ObtenerPorId(int id)
        {
            return _context.Devoluciones.Find(id);
        }

        public void Agregar(Devolucion devolucion)
        {
            _context.Devoluciones.Add(devolucion);
            _context.SaveChanges();
        }
    }
}
