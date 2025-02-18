using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CH_BACKEND.Repositories
{
    public class PagoRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public PagoRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public List<Pago> ObtenerTodos()
        {
            return _context.Pagos.Include(p => p.IdVentaNavigation).Include(p => p.IdMedioPagoNavigation).ToList();
        }

        public Pago? ObtenerPorId(int id)
        {
            return _context.Pagos.Include(p => p.IdVentaNavigation).Include(p => p.IdMedioPagoNavigation).FirstOrDefault(p => p.IdPago == id);
        }

        public void Agregar(Pago pago)
        {
            _context.Pagos.Add(pago);
            _context.SaveChanges();
        }
    }
}