namespace ModeladoATM;

public abstract class Transaccion
{
    public int NumeroCuenta { get; }
    public decimal Monto { get; }

    public Transaccion(int numeroCuenta, decimal monto)
    {
        NumeroCuenta = numeroCuenta;
        Monto = monto;
    }

    public abstract void Ejecutar(CuentaBancaria cuenta);
}