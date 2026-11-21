namespace ModeladoATM;

public class Retiro : Transaccion
{
    private DispensadorEfectivo dispensador;

    public Retiro(int numeroCuenta, decimal monto, DispensadorEfectivo dispensador)
        : base(numeroCuenta, monto)
    {
        this.dispensador = dispensador;
    }

    public override void Ejecutar(CuentaBancaria cuenta)
    {
        if (Monto <= 0)
        {
            Console.WriteLine(">> Error: El monto a retirar debe ser mayor que cero.");
            return;
        }

        if (Monto > cuenta.Saldo)
        {
            Console.WriteLine($">> Error: Fondos insuficientes. Su saldo disponible es: RD$ {cuenta.Saldo:N2}");
            return;
        }

        if (!dispensador.HayEfectivoSuficiente(Monto))
        {
            Console.WriteLine(">> Error: El cajero no dispone de suficiente efectivo físico en este momento.");
            return;
        }

        cuenta.Debitar(Monto);
        dispensador.DispensarEfectivo(Monto);
        Console.WriteLine($">> Retiro exitoso por RD$ {Monto:N2}. Saldo restante: RD$ {cuenta.Saldo:N2}");
    }
}