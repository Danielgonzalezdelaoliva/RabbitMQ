using MassTransit;
using Mq.Contratos.Comandos;
using Mq.Contratos.Eventos;

public class ConsumidorDelComandoCrearCita : IConsumer<ComandoCrearCita>
{
    public async Task Consume(ConsumeContext<ComandoCrearCita> context)
    {
        Console.WriteLine($"Simulacro de consumir el comando [{nameof(ComandoCrearCita)}]");

        // reaccionando al comando: imaginarse que aca hay un proceso de creacion de la cita. Simulacro por cuestiones de demo
        Console.WriteLine($"Creando Cita... {nameof(context.Message.CitaId)}: {context.Message.CitaId}");

        //Luego publicamos un mensaje de un evento cita creada
        //Imaginemos que hidratamos la informacion de la cita.
        //Es decir, no pasamos los id si no que los valores.
        //esto para evitar que el consumidor o consumidores hagan varias mismas llamadas para buscar el valor de cada Id
        await context.Publish<EventoDeIntegracionCitaCreada>(new EventoDeIntegracionCitaCreada
        {
            CitaId = context.Message.CitaId,
            Cliente = "John Doe",
            Peluquero = "Henry Smith",
            Tienda = "Peluqueria Principal",
            TipoDeCita = "Corte de Pelo",
            FechaDeLaCita = context.Message.Fecha,
            DuracionEnMinutos = context.Message.DuracionEnMinutos
        });
    }
}