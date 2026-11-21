namespace ModeladoATM;

public class CuentaBancaria
{
    public string IdHex { get; }
    public string NombreCompleto { get; }
    public int NumeroCuenta { get; }
    public string Pin { get; }
    public decimal Saldo { get; private set; }
    public string Ocupacion { get; }

    public CuentaBancaria(string idHex, string nombre, int noCuenta, string pin, decimal saldoInicial, string ocupacion)
    {
        IdHex = idHex;
        NombreCompleto = nombre;
        NumeroCuenta = noCuenta;
        Pin = pin;
        Saldo = saldoInicial;
        Ocupacion = ocupacion;
    }

    public bool ValidarPin(string pinIngresado) => Pin == pinIngresado;

    public void Acreditar(decimal monto) => Saldo += monto;

    public void Debitar(decimal monto) => Saldo -= monto;
}