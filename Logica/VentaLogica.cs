using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using CH_BACKEND.DBCalzadosHuancayo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class VentaLogica
    {
        private readonly VentaRepositorio _ventaRepositorio;

        public VentaLogica(VentaRepositorio ventaRepositorio)
        {
            _ventaRepositorio = ventaRepositorio;
        }

        public async Task<List<VentaResponse>> ObtenerVentas()
        {
            var ventas = await _ventaRepositorio.ObtenerTodas();
            return ventas.ConvertAll(v => new VentaResponse(v));
        }

        public async Task<VentaResponse?> ObtenerVentaPorId(int id)
        {
            var venta = await _ventaRepositorio.ObtenerPorId(id);
            return venta != null ? new VentaResponse(venta) : null;
        }

        // Crear una venta y sus detalles
        public async Task<VentaResponse?> AgregarVenta(VentaRequest request)
        {
            // Crear la venta principal
            var venta = new Venta
            {
                IdUsuario = request.IdUsuario,
                TipoComprobante = request.TipoComprobante,
                Fecha = request.Fecha,
                Total = request.Total,
                Estado = request.Estado,
                Serie = request.Serie,
                NumeroComprobante = request.NumeroComprobante,
                TotalIgv = request.TotalIgv,
                EstadoPago = "Pendiente",
                CostoEnvio = request.CostoEnvio
            };

            // Insertar la venta en la base de datos
            var ventaCreada = await _ventaRepositorio.Crear(venta);
            if (ventaCreada == null)
            {
                return null;
            }

            // Si se han enviado detalles, insertarlos
            if (request.Detalles != null && request.Detalles.Count > 0)
            {
                foreach (var detalleReq in request.Detalles)
                {
                    var detalle = new DetalleVenta
                    {
                        IdVenta = ventaCreada.IdVenta,
                        IdProducto = detalleReq.IdProducto,
                        Cantidad = detalleReq.Cantidad,
                        Precio = detalleReq.Precio,
                        Descuento = detalleReq.Descuento,
                        Total = detalleReq.Total,
                        IdUnidadMedida = detalleReq.IdUnidadMedida,
                        Igv = detalleReq.Igv
                    };

                    ventaCreada.DetalleVenta.Add(detalle);
                }
                // Guardar los cambios para insertar los detalles
                await _ventaRepositorio.GuardarCambiosAsync();
            }

            return new VentaResponse(ventaCreada);
        }

        public async Task<bool> ActualizarVenta(int id, VentaRequest request)
        {
            var ventaExistente = await _ventaRepositorio.ObtenerPorId(id);
            if (ventaExistente == null) return false;

            ventaExistente.TipoComprobante = request.TipoComprobante;
            ventaExistente.Fecha = request.Fecha;
            ventaExistente.Total = request.Total;
            ventaExistente.Estado = request.Estado;
            ventaExistente.Serie = request.Serie;
            ventaExistente.NumeroComprobante = request.NumeroComprobante;
            ventaExistente.TotalIgv = request.TotalIgv;
            ventaExistente.CostoEnvio = request.CostoEnvio;

            return await _ventaRepositorio.Actualizar(ventaExistente);
        }

        public async Task<bool> EliminarVenta(int id)
        {
            return await _ventaRepositorio.Eliminar(id);
        }
    }
}
