// CH_BACKEND/Logica/VentaLogica.cs
using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Models;
using CH_BACKEND.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CH_BACKEND.Logica
{
    public class VentaLogica
    {
        private readonly VentaRepositorio _ventaRepositorio;
        private readonly ProductoRepositorio _productoRepositorio;  // <-- agregado

        public VentaLogica(
            VentaRepositorio ventaRepositorio,
            ProductoRepositorio productoRepositorio      // <-- agregado
        )
        {
            _ventaRepositorio = ventaRepositorio;
            _productoRepositorio = productoRepositorio;   // <-- agregado
        }

        public async Task<List<VentaResponse>> ObtenerVentas()
        {
            var ventas = await _ventaRepositorio.ObtenerTodas();
            return ventas.Select(v => new VentaResponse(v)).ToList();
        }

        public async Task<VentaResponse?> ObtenerVentaPorId(int id)
        {
            var venta = await _ventaRepositorio.ObtenerPorId(id);
            return venta == null ? null : new VentaResponse(venta);
        }

        public async Task<VentaResponse?> AgregarVenta(VentaRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // 1) Crear cabecera de venta
            var venta = new Venta
            {
                IdUsuario = request.IdUsuario,
                TipoComprobante = request.TipoComprobante,
                Fecha = request.Fecha != default(DateTime) ? request.Fecha : DateTime.Now,
                Total = request.Total,
                Estado = request.Estado,
                Serie = request.Serie,
                NumeroComprobante = request.NumeroComprobante,
                TotalIgv = request.TotalIgv,
                EstadoPago = "Pendiente",
                CostoEnvio = request.CostoEnvio,
                MetodoEntrega = request.MetodoEntrega,
                SucursalRecoge = request.MetodoEntrega == "recoger_tienda"
                                    ? request.SucursalRecoge
                                    : null
            };

            var ventaCreada = await _ventaRepositorio.Crear(venta);
            if (ventaCreada == null) return null;

            // 2) Añadir detalles de venta
            if (request.Detalles != null && request.Detalles.Any())
            {
                foreach (var det in request.Detalles)
                {
                    ventaCreada.DetalleVenta.Add(new DetalleVenta
                    {
                        IdVenta = ventaCreada.IdVenta,
                        IdProducto = det.IdProducto,
                        Cantidad = det.Cantidad,
                        Precio = det.Precio,
                        Descuento = det.Descuento,
                        Total = det.Total,
                        IdUnidadMedida = det.IdUnidadMedida,
                        Igv = det.Igv
                    });
                }
                await _ventaRepositorio.GuardarCambiosAsync();
            }

            // 3) Reducir stock de cada producto vendido
            if (request.Detalles != null)
            {
                foreach (var det in request.Detalles)
                {
                    var producto = await _productoRepositorio.ObtenerPorId(det.IdProducto);
                    if (producto == null)
                        throw new Exception($"Producto {det.IdProducto} no existe");
                    if (producto.Stock < det.Cantidad)
                        throw new Exception($"Stock insuficiente para producto {producto.Nombre}");
                    producto.Stock -= det.Cantidad;
                    await _productoRepositorio.Actualizar(producto);
                }
            }

            // 4) Devolver respuesta
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
            ventaExistente.MetodoEntrega = request.MetodoEntrega;
            ventaExistente.SucursalRecoge = request.MetodoEntrega == "recoger_tienda"
                                                     ? request.SucursalRecoge
                                                     : null;

            return await _ventaRepositorio.Actualizar(ventaExistente);
        }

        public async Task<bool> EliminarVenta(int id)
            => await _ventaRepositorio.Eliminar(id);

        public async Task<bool> AñadirPagoAsync(int idVenta, PagoRequest req)
        {
            var venta = await _ventaRepositorio.ObtenerPorId(idVenta);
            if (venta == null) return false;

            var pago = new Pago
            {
                IdVenta = idVenta,
                MontoPagado = req.MontoPagado,
                FechaPago = req.FechaPago,
                IdMedioPago = req.IdMedioPago,
                IdTransaccionMP = req.IdTransaccionMP,
                EstadoPago = req.EstadoPago ?? "Pendiente",
                ComprobanteUrl = req.ComprobanteUrl
            };

            await _ventaRepositorio.AgregarPagoAsync(pago);
            venta.EstadoPago = pago.EstadoPago;
            await _ventaRepositorio.Actualizar(venta);

            return true;
        }

        public async Task<bool> CambiarEstadoAsync(int idVenta, EstadoRequest req)
        {
            var venta = await _ventaRepositorio.ObtenerPorId(idVenta);
            if (venta == null) return false;

            var hist = new VentaEstadoHistorial
            {
                IdVenta = idVenta,
                Estado = req.NuevoEstado,
                FechaCambio = DateTime.Now
            };
            await _ventaRepositorio.AgregarHistorialAsync(hist);

            venta.Estado = req.NuevoEstado;
            await _ventaRepositorio.Actualizar(venta);

            return true;
        }

        public async Task<PedidoDetalleResponse?> ObtenerPedidoDetalleCompleto(int idVenta, HttpRequest request)
        {
            var venta = await _ventaRepositorio.ObtenerConDetalle(idVenta);
            if (venta == null) return null;

            string baseUrl = $"{request.Scheme}://{request.Host}";
            var usuario = venta.IdUsuarioNavigation;
            var direccion = usuario.UsuarioDireccions
                                   .OrderByDescending(d => d.FechaRegistro)
                                   .FirstOrDefault();

            return new PedidoDetalleResponse
            {
                IdVenta = venta.IdVenta,
                Fecha = venta.Fecha,
                Total = venta.Total,
                Estado = venta.Estado,
                EstadoPago = venta.EstadoPago,
                CostoEnvio = venta.CostoEnvio,
                MetodoEntrega = venta.MetodoEntrega,
                SucursalRecoge = venta.SucursalRecoge,

                Cliente = new ClienteDto
                {
                    IdUsuario = usuario.IdUsuario,
                    NombreCompleto = usuario.NombreCompleto,
                    Telefono = usuario.Telefono,
                    Email = usuario.Email
                },

                DireccionEntrega = direccion == null
                    ? null
                    : new DireccionDto
                    {
                        IdDireccion = direccion.IdDireccion,
                        Direccion = direccion.Direccion,
                        Referencia = direccion.Referencia
                    },

                Detalles = venta.DetalleVenta.Select(d =>
                {
                    var foto = d.IdProductoNavigation.Foto?.TrimStart('/') ?? string.Empty;
                    var relativePath = foto.StartsWith("uploads/") ? foto : $"uploads/{foto}";
                    var imagenUrl = !string.IsNullOrEmpty(foto)
                        ? $"{baseUrl}/{relativePath}"
                        : null!;

                    return new DetalleDto
                    {
                        Id = d.Id,
                        IdProducto = d.IdProducto,
                        NombreProducto = d.IdProductoNavigation.Nombre,
                        ImagenUrl = imagenUrl,
                        Talla = d.IdUnidadMedidaNavigation?.Descripcion ?? string.Empty,
                        Cantidad = d.Cantidad,
                        Precio = d.Precio,
                        Total = d.Total
                    };
                }).ToList(),

                Pagos = venta.Pagos.Select(p => new PagoDto
                {
                    IdPago = p.IdPago,
                    IdMedioPago = p.IdMedioPago ?? 0,
                    EstadoPago = p.EstadoPago,
                    IdTransaccionMP = p.IdTransaccionMP,
                    FechaPago = p.FechaPago.ToDateTime(TimeOnly.MinValue),
                    ComprobanteUrl = p.ComprobanteUrl != null
                                       ? $"{baseUrl}{p.ComprobanteUrl}"
                                       : null
                }).ToList()
            };
        }
    }
}
