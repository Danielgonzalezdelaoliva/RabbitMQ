using GreenPipes;
using MassTransit;
using Mq.Contratos;
using Mq.Contratos.Eventos;

var busDeRabbitMq = Bus.Factory.CreateUsingRabbitMq(configuracion =>
{
    /*
     * Valores del configuracion.Host varian depende tu ambiente:
     * localhost = cuando no el app no usa docker y rabbitMq esta en container
     * host.dockler.internal = cuando la app usa docker
     * rabbitmqdemo = dar el "hostname" usado en docker-compose cuando usamos docker-compose para rabbitmq
     */
    configuracion.Host("localhost");
    configuracion.ReceiveEndpoint("confirmacion", e =>
    {
        e.UseInMemoryOutbox();
        e.Consumer<ConsumidorEventoCitaCreada>(c =>
        {
            c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10)));
        });
    });
});

var origenDelTokenDeCancelacion = new CancellationTokenSource(TimeSpan.FromSeconds(10));
await busDeRabbitMq.StartAsync(origenDelTokenDeCancelacion.Token);

//for efecto demo, cortando el tiempo de subscripcion
try
{
    while (true)
    {
        await Task.Delay(100);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
    throw;
}
finally
{
    await busDeRabbitMq.StopAsync();
    Console.WriteLine("Parando el bus");
}