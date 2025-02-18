using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CH_BACKEND.DBCalzadosHuancayo;


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
        return ventas.ConvertAll(v => new VentaResponse(v)); // Asegurar que se pase el tipo correcto
    }

    public async Task<VentaResponse?> ObtenerVentaPorId(int id)
    {
        var venta = await _ventaRepositorio.ObtenerPorId(id);
        return venta != null ? new VentaResponse(venta) : null; // Se corrigió el error
    }

    public async Task<bool> AgregarVenta(VentaRequest request)
    {
        // ID del cliente genérico (Clientes Varios)
        int idClienteGenerico = 1; // Asegúrate de que este ID exista en la base de datos

        var venta = new Venta
        {
            IdPersona = request.IdPersona > 0 ? request.IdPersona : idClienteGenerico,
            TipoComprobante = request.TipoComprobante,
            Fecha = request.Fecha,
            Total = request.Total,
            Estado = request.Estado,
            Serie = request.Serie,
            NumeroComprobante = request.NumeroComprobante,
            TotalIgv = request.TotalIgv
        };

        return await _ventaRepositorio.Crear(venta);
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

        return await _ventaRepositorio.Actualizar(ventaExistente);
    }

    public async Task<bool> EliminarVenta(int id)
    {
        return await _ventaRepositorio.Eliminar(id);
    }
}
