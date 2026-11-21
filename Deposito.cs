namespace ModeladoATM;

// APLICACIÓN DE HERENCIA: Deposito "ES UNA" Transaccion
public class Deposito : Transaccion
{
    // Reutiliza el constructor padre mediante ': base(...)'
    public Deposito(int numeroCuenta, decimal monto)
        : base(numeroCuenta, monto)
    {
    }

    // Firma idéntica a Transaccion.cs: bool Ejecutar(ref decimal saldoCuenta)
    public override bool Ejecutar(ref decimal saldoCuenta)
    {
        Console.WriteLine($"\n>> Procesando depósito por RD$ {Monto:N2}...");

        if (Monto <= 0)
        {
            Console.WriteLine(">> Error: El monto a depositar debe ser mayor que cero.");
            return false;
        }

        // Incremento contable al saldo en memoria
        saldoCuenta += Monto;
        Console.WriteLine($">> Depósito completado con éxito. Nuevo saldo en cuenta: RD$ {saldoCuenta:N2}");
        return true;
    }
}