using CH_BACKEND.DBCalzadosHuancayo;

namespace CH_BACKEND.Models
{
    public class DevolucionResponse
    {
        public int IdDevolucion { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public decimal CantidadDevolucion { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Motivo { get; set; }

        public DevolucionResponse(Devolucion devolucion)
        {
            IdDevolucion = devolucion.IdDevolucion;
            IdVenta = devolucion.IdVenta;
            IdProducto = devolucion.IdProducto;
            CantidadDevolucion = devolucion.CantidadDevolucion;
            Fecha = devolucion.Fecha;
            Motivo = devolucion.Motivo;
        }
    }
}
