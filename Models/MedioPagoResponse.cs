using CH_BACKEND.Models;

public class MedioPagoResponse
{
    public int IdMedioPago { get; set; }
    public string Descripcion { get; set; }
    public string Titular { get; set; }
    public List<PagoResponse> Pagos { get; set; }
}
