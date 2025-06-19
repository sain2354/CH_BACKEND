using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class DireccionEntregaLogica
    {
        private readonly DireccionEntregaRepository _repo;

        public DireccionEntregaLogica(DireccionEntregaRepository repo)
        {
            _repo = repo;
        }

        // Obtener todas
        public async Task<List<DireccionEntregaResponse>> ObtenerTodas()
        {
            var lista = await _repo.ObtenerTodas();
            return lista.Select(d => new DireccionEntregaResponse(d)).ToList();
        }

        // Obtener por id
        public async Task<DireccionEntregaResponse?> ObtenerPorId(int id)
        {
            var entidad = await _repo.ObtenerPorId(id);
            if (entidad == null) return null;
            return new DireccionEntregaResponse(entidad);
        }

        // Crear
        public async Task<DireccionEntregaResponse> Crear(DireccionEntregaRequest request)
        {
            var entidad = new DireccionEntrega
            {
                IdVenta = request.IdVenta,
                Direccion = request.Direccion,
                Lat = request.Lat,
                Lng = request.Lng,
                Referencia = request.Referencia,
                CostoEnvio = request.CostoEnvio
            };

            var creada = await _repo.Crear(entidad);
            return new DireccionEntregaResponse(creada);
        }

        // Actualizar
        public async Task<bool> Actualizar(int id, DireccionEntregaRequest request)
        {
            var entidad = await _repo.ObtenerPorId(id);
            if (entidad == null) return false;

            entidad.IdVenta = request.IdVenta;
            entidad.Direccion = request.Direccion;
            entidad.Lat = request.Lat;
            entidad.Lng = request.Lng;
            entidad.Referencia = request.Referencia;
            entidad.CostoEnvio = request.CostoEnvio;

            return await _repo.Actualizar(entidad);
        }

        // Eliminar
        public async Task<bool> Eliminar(int id)
        {
            return await _repo.Eliminar(id);
        }
    }
}
