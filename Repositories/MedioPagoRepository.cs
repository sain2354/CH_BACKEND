using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CH_BACKEND.Repositories
{
    public class MedioPagoRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public MedioPagoRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public List<MedioPago> ObtenerTodos()
        {
            return _context.MedioPagos.ToList();
        }

        public MedioPago? ObtenerPorId(int id)
        {
            return _context.MedioPagos.FirstOrDefault(mp => mp.IdMedioPago == id);
        }

        public void Agregar(MedioPago medioPago)
        {
            _context.MedioPagos.Add(medioPago);
            _context.SaveChanges();
        }

        public void Actualizar(MedioPago medioPago)
        {
            _context.MedioPagos.Update(medioPago);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var medioPago = _context.MedioPagos.Find(id);
            if (medioPago != null)
            {
                _context.MedioPagos.Remove(medioPago);
                _context.SaveChanges();
            }
        }
    }
}
