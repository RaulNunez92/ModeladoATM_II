namespace ModeladoATM;

// APLICACIÓN DE HERENCIA: Retiro "ES UNA" Transaccion
public class Retiro : Transaccion
{
    // Referencia al dispensador para coordinar la entrega física
    private readonly DispensadorEfectivo _dispensador;

    // Uso de ': base(...)' para invocar el constructor de Transaccion
    public Retiro(int numeroCuenta, decimal monto, DispensadorEfectivo dispensador)
        : base(numeroCuenta, monto)
    {
        _dispensador = dispensador ?? throw new ArgumentNullException(nameof(dispensador));
    }

    // Implementación concreta del método abstracto heredado
    public override bool Ejecutar(ref decimal saldoCuenta)
    {
        Console.WriteLine($"\n>> Validando transacción de retiro por RD$ {Monto:N2}...");

        // 1. Validación contable (Fondos en la cuenta del cliente)
        if (Monto > saldoCuenta)
        {
            Console.WriteLine($">> Error: Fondos insuficientes. Su saldo actual es: RD$ {saldoCuenta:N2}");
            return false;
        }

        // 2. Validación de hardware (Efectivo físico disponible en la máquina)
        if (!_dispensador.HayEfectivoSuficiente(Monto))
        {
            Console.WriteLine(">> Error: El cajero no dispone de suficiente efectivo físico para procesar este retiro.");
            return false;
        }

        // 3. Ejecución exitosa: se actualizan el saldo lógico y el efectivo físico
        saldoCuenta -= Monto;
        _dispensador.DispensarEfectivo(Monto);
        Console.WriteLine($">> Retiro exitoso. Saldo restante en cuenta: RD$ {saldoCuenta:N2}");
        return true;
    }
}