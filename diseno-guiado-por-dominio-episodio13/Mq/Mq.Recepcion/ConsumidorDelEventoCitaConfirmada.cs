using MassTransit;
using Mq.Contratos.Eventos;

public class ConsumidorDelEventoCitaConfirmada : IConsumer<EventoDeIntegracionCitaConfirmada>
{  
    public async Task Consume(
        ConsumeContext<EventoDeIntegracionCitaConfirmada> context)
    {
        await Task.Run(() =>
            Console.WriteLine("Simulacro cambiando el status de una cita a Confirmado"));
    }
}