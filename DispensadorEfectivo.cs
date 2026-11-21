namespace ModeladoATM;

public class DispensadorEfectivo
{
    private decimal efectivoDisponible;

    public DispensadorEfectivo(decimal efectivoInicial)
    {
        efectivoDisponible = efectivoInicial;
    }

    public bool HayEfectivoSuficiente(decimal monto) => efectivoDisponible >= monto;

    public void DispensarEfectivo(decimal monto)
    {
        efectivoDisponible -= monto;
        Console.WriteLine($"[Hardware Dispensador]: Expulsando RD$ {monto:N2} en billetes físicos.");
    }
}