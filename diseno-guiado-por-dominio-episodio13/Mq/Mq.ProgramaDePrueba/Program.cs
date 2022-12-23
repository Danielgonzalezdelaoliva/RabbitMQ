using GreenPipes;
using MassTransit;
using Mq.Contratos.Comandos;

Console.WriteLine("Programa de creacion de citas...");
//esperando que los servicios enciendan y esten listos
await Task.Delay(3000);

var busDeRabbitMq = Bus.Factory.CreateUsingRabbitMq(configuracion =>
{
    /*
     * Valores del configuracion.Host varian depende tu ambiente:
     * localhost = cuando no el app no usa docker y rabbitMq esta en container
     * host.dockler.internal = cuando la app usa docker
     * rabbitmqdemo = dar el "hostname" usado en docker-compose cuando usamos docker-compose para rabbitmq
     */
    configuracion.Host("localhost");
    configuracion.ReceiveEndpoint("programa-prueba", e =>
    {
        e.UseInMemoryOutbox();
        e.Consumer<ConsumidorDelEventoCitaConfirmada>(c =>
        {
            c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10)));
        });
    });
});

var origenDelTokenDeCancelacion = new CancellationTokenSource(TimeSpan.FromSeconds(10));
await busDeRabbitMq.StartAsync(origenDelTokenDeCancelacion.Token);

try
{
    Console.WriteLine("Presione cualquier tecla para crear una cita...");
    Console.ReadLine();
    await EnviarMensajeConComandoParaCrearCita(busDeRabbitMq);
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
    throw;
}
finally
{
    // await busDeRabbitMq.StopAsync();
    // Console.WriteLine("Parando el bus");
}

static async Task EnviarMensajeConComandoParaCrearCita(IPublishEndpoint publicador)
{
    Console.WriteLine($"Publicando evento con comando: {nameof(ComandoCrearCita)}");
    await publicador.Publish<ComandoCrearCita>(new ComandoCrearCita
    {
        TiendaId = Guid.NewGuid(),
        ClienteId = Guid.NewGuid(),
        CitaId = Guid.NewGuid(),
        PeluqueroId = Guid.NewGuid(),
        TipoDeCitaId = Guid.NewGuid(),
        DuracionEnMinutos = 60,
        Fecha = DateTime.Now.AddDays(1).AddHours(1)
    });
}
