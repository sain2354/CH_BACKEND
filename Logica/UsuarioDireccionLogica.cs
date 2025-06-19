using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using CH_BACKEND.DBCalzadosHuancayo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CH_BACKEND.Controllers;

namespace CH_BACKEND.Logica
{
    public class UsuarioDireccionLogica
    {
		private readonly UsuarioDireccionRepository _repo;

		public UsuarioDireccionLogica(UsuarioDireccionRepository repo)
		{
			_repo = repo;
		}

		// Obtener todas (opcional)
		public async Task<List<UsuarioDireccionResponse>> ObtenerTodas()
		{
			var lista = await _repo.ObtenerTodas();
			return lista.Select(d => new UsuarioDireccionResponse(d)).ToList();
		}

		// Obtener direcciones de un usuario
		public async Task<List<UsuarioDireccionResponse>> ObtenerPorUsuario(int idUsuario)
		{
			var lista = await _repo.ObtenerPorUsuario(idUsuario);
			return lista.Select(d => new UsuarioDireccionResponse(d)).ToList();
		}

		// Obtener una dirección por Id
		public async Task<UsuarioDireccionResponse?> ObtenerPorId(int idDireccion)
		{
			var entidad = await _repo.ObtenerPorId(idDireccion);
			if (entidad == null) return null;
			return new UsuarioDireccionResponse(entidad);
		}

		// Crear nueva dirección
		public async Task<UsuarioDireccionResponse> Crear(UsuarioDireccionRequest request)
		{
			var entidad = new UsuarioDireccion
			{
				IdUsuario = request.IdUsuario,
				Direccion = request.Direccion,
				Lat = request.Lat,
				Lng = request.Lng,
				Referencia = request.Referencia
			};

			var creada = await _repo.Crear(entidad);
			return new UsuarioDireccionResponse(creada);
		}

		// Actualizar
		public async Task<bool> Actualizar(int idDireccion, UsuarioDireccionRequest request)
		{
			var entidad = await _repo.ObtenerPorId(idDireccion);
			if (entidad == null) return false;

			entidad.IdUsuario = request.IdUsuario;
			entidad.Direccion = request.Direccion;
			entidad.Lat = request.Lat;
			entidad.Lng = request.Lng;
			entidad.Referencia = request.Referencia;
			// FechaRegistro no se cambia usualmente

			return await _repo.Actualizar(entidad);
		}

		// Eliminar
		public async Task<bool> Eliminar(int idDireccion)
		{
			return await _repo.Eliminar(idDireccion);
		}
	}
}
