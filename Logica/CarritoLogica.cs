using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Models;
using CH_BACKEND.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class CarritoLogica
    {
        private readonly CarritoRepositorio _carritoRepositorio;

        public CarritoLogica(CarritoRepositorio carritoRepositorio)
        {
            _carritoRepositorio = carritoRepositorio;
        }

        public async Task<List<CarritoResponse>> ObtenerTodos()
        {
            var lista = await _carritoRepositorio.ObtenerTodos();
            var response = new List<CarritoResponse>();

            foreach (var c in lista)
            {
                response.Add(MapToResponse(c));
            }
            return response;
        }

        public async Task<CarritoResponse> ObtenerPorId(int idCarrito)
        {
            var carrito = await _carritoRepositorio.ObtenerPorId(idCarrito);
            if (carrito == null) return null;
            return MapToResponse(carrito);
        }

        public async Task<bool> Crear(CarritoRequest request)
        {
            var carrito = new Carrito
            {
                IdUsuario = request.IdUsuario,
                FechaCreacion = request.FechaCreacion ?? DateTime.Now
            };

            return await _carritoRepositorio.Crear(carrito);
        }

        public async Task<bool> Actualizar(int idCarrito, CarritoRequest request)
        {
            var carritoExistente = await _carritoRepositorio.ObtenerPorId(idCarrito);
            if (carritoExistente == null) return false;

            carritoExistente.IdUsuario = request.IdUsuario;
            if (request.FechaCreacion.HasValue)
                carritoExistente.FechaCreacion = request.FechaCreacion.Value;

            return await _carritoRepositorio.Actualizar(carritoExistente);
        }

        public async Task<bool> Eliminar(int idCarrito)
        {
            return await _carritoRepositorio.Eliminar(idCarrito);
        }

        private CarritoResponse MapToResponse(Carrito c)
        {
            return new CarritoResponse
            {
                IdCarrito = c.IdCarrito,
                IdUsuario = c.IdUsuario,
                FechaCreacion = c.FechaCreacion
            };
        }
    }
}
