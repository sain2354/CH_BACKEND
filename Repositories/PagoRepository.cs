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
            return _context.Pagos
                .Include(p => p.IdVentaNavigation)
                .ToList();
        }

        public Pago? ObtenerPorId(int id)
        {
            return _context.Pagos
                .Include(p => p.IdVentaNavigation)
                .FirstOrDefault(p => p.IdPago == id);
        }

        public void Agregar(Pago pago)
        {
            _context.Pagos.Add(pago);
            _context.SaveChanges();
        }

        public void Actualizar(Pago pago)
        {
            _context.Pagos.Update(pago);
            _context.SaveChanges();
        }

        public void Eliminar(Pago pago)
        {
            _context.Pagos.Remove(pago);
            _context.SaveChanges();
        }
    }
}
