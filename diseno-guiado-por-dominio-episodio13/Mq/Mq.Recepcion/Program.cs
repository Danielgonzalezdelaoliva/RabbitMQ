using GreenPipes;
using MassTransit;
var busDeRabbitMq = Bus.Factory.CreateUsingRabbitMq(configuracion =>
{
    /*
     * Valores del configuracion.Host varian depende tu ambiente:
     * localhost = cuando no el app no usa docker y rabbitMq esta en container
     * host.dockler.internal = cuando la app usa docker
     * rabbitmqdemo = dar el "hostname" usado en docker-compose cuando usamos docker-compose para rabbitmq
     */
    configuracion.Host("localhost");
    configuracion.ReceiveEndpoint("recepcion", e =>
    {
        e.UseInMemoryOutbox();
        e.Consumer<ConsumidorDelComandoCrearCita>(c =>
        {
            c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10)));
        });
    });
    configuracion.ReceiveEndpoint("recepcion-confirmacion", e =>
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

//for efecto demo, cortando el tiempo de subscripcion
try
{
    while (true)
    {
        await Task.Delay(200);
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
