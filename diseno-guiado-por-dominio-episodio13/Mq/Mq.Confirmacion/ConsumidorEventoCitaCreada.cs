using MassTransit;
using Mq.Contratos.Eventos;

public class ConsumidorEventoCitaCreada : IConsumer<EventoDeIntegracionCitaCreada>
{
    public async Task Consume(
        ConsumeContext<EventoDeIntegracionCitaCreada> context)
    {
        Console.WriteLine($"Simulacro de enviar correo electronico al cliente: {context.Message.Cliente}");
        Console.WriteLine("Cita confirmada por cliente!");

        Console.WriteLine($"Enviando mensage {nameof(EventoDeIntegracionCitaConfirmada)}");

        await context.Publish<EventoDeIntegracionCitaConfirmada>(new EventoDeIntegracionCitaConfirmada {
            CitaId = context.Message.CitaId            
        });

    }
}