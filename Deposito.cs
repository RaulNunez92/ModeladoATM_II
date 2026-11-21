namespace ModeladoATM;

public class Deposito : Transaccion
{
    public Deposito(int numeroCuenta, decimal monto)
        : base(numeroCuenta, monto)
    {
    }

    public override void Ejecutar(CuentaBancaria cuenta)
    {
        if (Monto <= 0)
        {
            Console.WriteLine(">> Error: El monto a depositar debe ser mayor que cero.");
            return;
        }

        cuenta.Acreditar(Monto);
        Console.WriteLine($">> Depósito exitoso de RD$ {Monto:N2}. Nuevo saldo disponible: RD$ {cuenta.Saldo:N2}");
    }
}