namespace ModeladoATM;

// APLICACIÓN DE COMPOSICIÓN: ATM "TIENE UN" DispensadorEfectivo
public class ATM
{
    private readonly DispensadorEfectivo _dispensador;
    private int _numeroCuenta;
    private decimal _saldoCuenta;

    public ATM(decimal fondoCajeroInicial)
    {
        // Composición: el ATM crea y administra el ciclo de vida del dispensador
        _dispensador = new DispensadorEfectivo(fondoCajeroInicial);
    }

    // Método principal interactivo para la simulación
    public void Iniciar()
    {
        Console.Clear();
        Console.WriteLine("==========================================================");
        Console.WriteLine("            SISTEMA DE CAJERO AUTOMÁTICO (DEITEL)         ");
        Console.WriteLine("==========================================================");

        // Captura dinámica interactiva de la cuenta del usuario
        _numeroCuenta = CapturarEntero("Ingrese su Número de Cuenta: ");
        _saldoCuenta = CapturarDecimal("Ingrese el Saldo Inicial disponible en la cuenta: RD$ ");

        bool enSesion = true;

        do
        {
            Console.WriteLine("\n----------------------------------------------------------");
            Console.WriteLine($" SESIÓN ACTIVA | Cuenta: #{_numeroCuenta} | Saldo: RD$ {_saldoCuenta:N2}");
            Console.WriteLine($" Reserva física en Cajero: RD$ {_dispensador.EfectivoDisponible:N2}");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("1. Realizar Retiro de Efectivo");
            Console.WriteLine("2. Consultar Saldo Actual");
            Console.WriteLine("3. Finalizar Sesión");
            Console.Write("Seleccione una opción (1-3): ");

            string opcion = Console.ReadLine() ?? "";

            switch (opcion)
            {
                case "1":
                    decimal montoRetiro = CapturarDecimal("\nIngrese el monto a retirar: RD$ ");
                    Retiro transaccionRetiro = new Retiro(_numeroCuenta, montoRetiro, _dispensador);
                    transaccionRetiro.Ejecutar(ref _saldoCuenta);
                    Pausar();
                    break;

                case "2":
                    decimal montoDeposito = CapturarDecimal("\nIngrese el monto a depositar: RD$ ");
                    Deposito transaccionDeposito = new Deposito(_numeroCuenta, montoDeposito);
                    transaccionDeposito.Ejecutar(ref _saldoCuenta);
                    Pausar();
                    break;

                case "3":
                    Console.WriteLine($"\n>> Saldo contable disponible: RD$ {_saldoCuenta:N2}");
                    Pausar();
                    break;

                case "4":
                    Console.WriteLine("\n>> Sesión finalizada. Gracias por utilizar nuestro cajero.");
                    enSesion = false;
                    break;
            }

        } while (enSesion);
    }

    // Métodos auxiliares de captura defensiva (evitan excepciones no controladas)
    private static int CapturarEntero(string prompt)
    {
        int resultado;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out resultado) || resultado <= 0)
        {
            Console.Write(">> Entrada no válida. Ingrese un número de cuenta positivo: ");
        }
        return resultado;
    }

    private static decimal CapturarDecimal(string prompt)
    {
        decimal resultado;
        Console.Write(prompt);
        while (!decimal.TryParse(Console.ReadLine(), out resultado) || resultado <= 0)
        {
            Console.Write(">> Entrada no válida. Ingrese un monto positivo: ");
        }
        return resultado;
    }

    private static void Pausar()
    {
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
    }
}