﻿using CH_BACKEND.Models;

public class MedioPagoRequest
{
    public string Descripcion { get; set; }
    public string Titular { get; set; }
    public List<PagoRequest> Pagos { get; set; }
}
