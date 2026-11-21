namespace ModeladoATM;

// CLASE BASE: Abstracción general de cualquier operación financiera
public abstract class Transaccion
{
    // Encapsulamiento: lectura pública, modificación restringida
    public int NumeroCuenta { get; protected set; }
    public decimal Monto { get; protected set; }

    // Constructor base que obliga a inicializar los atributos esenciales
    public Transaccion(int numeroCuenta, decimal monto)
    {
        if (numeroCuenta <= 0)
            throw new ArgumentException("El número de cuenta debe ser válido y positivo.");

        if (monto <= 0)
            throw new ArgumentException("El monto de la transacción debe ser mayor que cero.");

        NumeroCuenta = numeroCuenta;
        Monto = monto;
    }

    // Método polimórfico que cada tipo de transacción implementará de forma especializada
    public abstract bool Ejecutar(ref decimal saldoCuenta);
}
