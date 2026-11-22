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

    // Método principal interactivo
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
            Console.WriteLine("1. Consultar Saldo Actual");
            Console.WriteLine("2. Realizar Depósito de Efectivo");
            Console.WriteLine("3. Realizar Retiro de Efectivo");
            Console.WriteLine("4. Finalizar Sesión");
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Seleccione una opción (1-4): ");

            string opcion = Console.ReadLine() ?? "";

            switch (opcion)
            {
                case "1": // Consultar Saldo
                    Console.WriteLine($"\n>> Saldo contable disponible: RD$ {_saldoCuenta:N2}");
                    Pausar();
                    break;

                case "2": // Depositar Dinero
                    decimal montoDeposito = CapturarDecimal("\nIngrese el monto a depositar: RD$ ");
                    Deposito transaccionDeposito = new Deposito(_numeroCuenta, montoDeposito);
                    transaccionDeposito.Ejecutar(ref _saldoCuenta);
                    Pausar();
                    break;

                case "3": // Retirar Dinero
                    decimal montoRetiro = CapturarDecimal("\nIngrese el monto a retirar: RD$ ");
                    Retiro transaccionRetiro = new Retiro(_numeroCuenta, montoRetiro, _dispensador);
                    transaccionRetiro.Ejecutar(ref _saldoCuenta);
                    Pausar();
                    break;

                case "4": // Salir
                    Console.WriteLine("\n>> Sesión finalizada. Gracias por utilizar nuestro cajero.");
                    enSesion = false;
                    break;

                default:
                    Console.WriteLine(">> Error: Opción no reconocida. Seleccione una opción entre 1 y 4.");
                    Pausar();
                    break;
            }

        } while (enSesion);
    }

    // Métodos auxiliares de captura defensiva (evitan excepciones fatales)
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