# Sistema de Cajero Automático (ATM II) — Modelado con Herencia y Composición

Implementación orientada a objetos en C# (.NET 8) basada en el modelo conceptual de cajero automático de **Deitel & Deitel**.

## 📌 Enlace al Repositorio
- **GitHub:** [https://github.com/RaulNunez92/ModeladoATM_II](https://github.com/RaulNunez92/ModeladoATM_II)

---

## 📐 Arquitectura de Software y Relaciones POO
El sistema implementa de forma pura las dos relaciones fundamentales del diseño orientado a objetos:
1. **Herencia (`is-a`):** Las clases `Retiro` y `Deposito` heredan de la clase base abstracta `Transaccion`, reutilizando propiedades esenciales e implementando el método polimórfico `Ejecutar(ref decimal saldo)`.
2. **Composición (`has-a`):** La clase `ATM` contiene e instancia de manera privada un componente `DispensadorEfectivo`, controlando la reserva de billetes físicos de la máquina.
3. **Cero Valores Fijos (No Hardcoding):** El sistema es 100% interactivo mediante captura en consola validada con programación defensiva (`int.TryParse` y `decimal.TryParse`).

---

## 📊 Diagrama de Clases
*(Diagrama generado con PlantUML)*

```
       +-------------------------------+
       |          Transaccion          | <--- (Clase Base Abstracta)
       +-------------------------------+
       | + NumeroCuenta: int           |
       | + Monto: decimal              |
       +-------------------------------+
       | + Ejecutar(ref saldo): bool   |
       +-------------------------------+
                 ^           ^
                 | (es-un)   | (es-un)
        ┌────────┴──────┐    └──────────────┐
        │               │                   │
+---------------+ +---------------+ +----------------------------------+
|    Retiro     | |   Deposito    | |       DispensadorEfectivo        |
+---------------+ +---------------+ +----------------------------------+
| - dispensador |                   | - efectivoDisponible: decimal    |
+---------------+                   +----------------------------------+
        │ (usa para billetes)                       ^
        └───────────────────────────────────────────┤ (tiene-un)
                                                    │
                                            +---------------+
                                            |      ATM      |
                                            +---------------+
```

---

## 🚀 Instrucciones de Ejecución

### Prerrequisitos:
- SDK de .NET 8.0 instalado.

### Comandos de consola:
```bash
# Compilar y ejecutar la simulación interactiva
dotnet run
```

---

## 📸 Evidencias de Casos de Prueba

### Caso 1: Retiro Exitoso
El monto solicitado es menor o igual al saldo de la cuenta y al efectivo del dispensador.
![Retiro Exitoso](./docs/evidencia_retiro_exitoso.png)

### Caso 2: Depósito Exitoso
El monto a depositar es mayor a cero y se suma de forma inmediata al saldo disponible.
![Depósito Exitoso](./docs/evidencia_deposito_exitoso.png)

### Caso 3: Rechazo por Fondos Insuficientes
El usuario intenta retirar un monto superior al saldo actual de su cuenta.
![Fondos Insuficientes](./docs/evidencia_fondos_insuficientes.png)

### Caso 4: Rechazo por Falta de Efectivo en el Dispensador
El cajero físico no cuenta con suficiente dinero en su reserva para cubrir la solicitud.
![Dispensador Vacío](./docs/evidencia_dispensador_vacio.png)