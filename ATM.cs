namespace ModeladoATM
{
    public class ATM
    {
        // Composición: el ATM tiene un dispensador y la base de datos de cuentas
        private readonly DispensadorEfectivo dispensador;
        private List<CuentaBancaria> baseDeDatosCuentas = new List<CuentaBancaria>();

        public ATM()
        {
            dispensador = new DispensadorEfectivo(20000m); // RD$ 20,000 en el cajero
            InicializarCuentas();
        }

        private void InicializarCuentas()
        {
            baseDeDatosCuentas = new List<CuentaBancaria>
            {
                new CuentaBancaria("0xA12F", "Raynieri Raúl Núñez", 1001, "1234", 55000.00m, "Desarrollador de Software"),
                new CuentaBancaria("0xB34C", "Carlos Manuel Tejeda", 1002, "4321", 82500.00m, "Ingeniero Civil"),
                new CuentaBancaria("0xC78D", "Laura Patricia Gómez", 1003, "7890", 34200.00m, "Diseñadora Gráfica"),
                new CuentaBancaria("0xD91E", "Miguel Ángel Rosario", 1004, "5566", 67800.00m, "Contador Público"),
                new CuentaBancaria("0xE45A", "Carmen Rosa Morales", 1005, "9988", 95000.00m, "Médica Pediatra")
            };
        }

        public void Iniciar()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==========================================================");
                Console.WriteLine("            BANCO NACIONAL - SISTEMA ATM II               ");
                Console.WriteLine("==========================================================");

                Console.Write("\nIngrese su Número de Cuenta (o '0' para apagar): ");
                if (!int.TryParse(Console.ReadLine(), out int noCuenta) || noCuenta == 0)
                {
                    Console.WriteLine("Cajero en modo reposo. ¡Hasta pronto!");
                    break;
                }

                // Búsqueda en la colección de cuentas
                CuentaBancaria? cuentaAutenticada = baseDeDatosCuentas.Find(c => c.NumeroCuenta == noCuenta);

                if (cuentaAutenticada == null)
                {
                    Console.WriteLine(">> Error: Número de cuenta no registrado en la institución.");
                    EsperarTecla();
                    continue;
                }

                Console.Write("Ingrese su PIN de 4 dígitos: ");
                string pinIngresado = Console.ReadLine() ?? "";

                if (!cuentaAutenticada.ValidarPin(pinIngresado))
                {
                    Console.WriteLine(">> Error: PIN de seguridad incorrecto.");
                    EsperarTecla();
                    continue;
                }

                // Sesión autenticada exitosa
                EjecutarSesionCliente(cuentaAutenticada);
            }
        }

        private void EjecutarSesionCliente(CuentaBancaria cuenta)
        {
            bool enSesion = true;

            do
            {
                Console.Clear();
                Console.WriteLine("==========================================================");
                Console.WriteLine($" BIENVENIDO(A): {cuenta.NombreCompleto.ToUpper()}");
                Console.WriteLine($" ID Único: [{cuenta.IdHex}] | Ocupación: {cuenta.Ocupacion}");
                Console.WriteLine($" No. Cuenta: {cuenta.NumeroCuenta}");
                Console.WriteLine("==========================================================");
                Console.WriteLine("1. Consultar Saldo");
                Console.WriteLine("2. Retirar Efectivo");
                Console.WriteLine("3. Depositar Efectivo");
                Console.WriteLine("4. Finalizar y Retirar Tarjeta");
                Console.WriteLine("----------------------------------------------------------");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine($"\n>> Su saldo disponible actual es: RD$ {cuenta.Saldo:N2}");
                        EsperarTecla();
                        break;

                    case "2":
                        decimal montoRetiro = CapturarMonto("\nIngrese el monto a retirar: RD$ ");
                        Retiro transaccionRetiro = new Retiro(cuenta.NumeroCuenta, montoRetiro, dispensador);
                        transaccionRetiro.Ejecutar(cuenta);
                        EsperarTecla();
                        break;

                    case "3":
                        decimal montoDeposito = CapturarMonto("\nIngrese el monto a depositar: RD$ ");
                        Deposito transaccionDeposito = new Deposito(cuenta.NumeroCuenta, montoDeposito);
                        transaccionDeposito.Ejecutar(cuenta);
                        EsperarTecla();
                        break;

                    case "4":
                        Console.WriteLine("\n>> Gracias por preferir nuestros servicios. No olvide su comprobante.");
                        enSesion = false;
                        EsperarTecla();
                        break;

                    default:
                        Console.WriteLine(">> Error: Opción no válida. Elija del 1 al 4.");
                        EsperarTecla();
                        break;
                }

            } while (enSesion);
        }

        private decimal CapturarMonto(string mensaje)
        {
            decimal monto;
            Console.Write(mensaje);
            while (!decimal.TryParse(Console.ReadLine(), out monto) || monto <= 0)
            {
                Console.WriteLine(">> Error: Ingrese un valor monetario numérico mayor a cero.");
                Console.Write(mensaje);
            }
            return monto;
        }

        private void EsperarTecla()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}