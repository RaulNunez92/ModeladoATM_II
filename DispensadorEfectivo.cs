namespace ModeladoATM;

// COMPONENTE: Representa el hardware dispensador de billetes
public class DispensadorEfectivo
{
    // Encapsulamiento estricto: nadie fuera de esta clase puede alterar el dinero físico directamente
    private decimal _efectivoDisponible;

    public decimal EfectivoDisponible => _efectivoDisponible;

    public DispensadorEfectivo(decimal efectivoInicial)
    {
        if (efectivoInicial < 0)
            throw new ArgumentException("El efectivo inicial del dispensador no puede ser negativo.");

        _efectivoDisponible = efectivoInicial;
    }

    // Regla de verificación física de billetes
    public bool HayEfectivoSuficiente(decimal monto)
    {
        return _efectivoDisponible >= monto;
    }

    // Expulsión física de billetes
    public void DispensarEfectivo(decimal monto)
    {
        _efectivoDisponible -= monto;
        Console.WriteLine($"[Hardware Dispensador]: Expulsando RD$ {monto:N2} en efectivo.");
    }
}