using MassTransit;
using Mq.Contratos.Eventos;

public class ConsumidorDelEventoCitaConfirmada : IConsumer<EventoDeIntegracionCitaConfirmada>
{
    public async Task Consume(
        ConsumeContext<EventoDeIntegracionCitaConfirmada> context)
    {
        await Task.Run(() =>
            Console.WriteLine($"La cita {context.Message.CitaId} has sido confirmada"));
    }
}