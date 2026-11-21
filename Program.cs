namespace ModeladoATM;

internal class Program
{
    static void Main(string[] args)
    {
        // Se inicializa el cajero con un fondo físico de RD$ 100,000.00 en su dispensador
        ATM cajero = new ATM(100000m);
        cajero.Iniciar();
    }
}