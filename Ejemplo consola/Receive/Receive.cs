using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receive;
class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                #region "subscribir los mensajes SIN exchange"
                // //crear o declara la cola
                // channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                // //creamos un call back
                // var consumer = new EventingBasicConsumer(channel);

                // //creamos un delegado
                // consumer.Received += (model, ea) =>
                // {
                //     //obtenemos la lista de bytes que han sido transmitidos por el cola
                //     var body = ea.Body.ToArray();
                //     var message = Encoding.UTF8.GetString(body);

                //     Console.WriteLine($"[x] Received {message}");

                // };

                // //autoAck: indicar a la aplicación que envia que el mensaje ha sido recibido
                // channel.BasicConsume(queue: "hello", autoAck: true,consumer:consumer);

                // Console.WriteLine("Press any key to exit...");
                // Console.ReadLine();
                #endregion

                #region "subscribir los mensajes CON exchange"
                
                channel.ExchangeDeclare(exchange:"logs", type:ExchangeType.Fanout);

                //declaramos las colas de envío y las subscrinmos al exchange
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue:queueName,exchange:"logs",routingKey:"");

                Console.WriteLine("Waiting for logs...");

                //creamos un call back
                var consumer = new EventingBasicConsumer(channel);

                //creamos un delegado
                consumer.Received += (model, ea) =>
                {
                    //obtenemos la lista de bytes que han sido transmitidos por el cola
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"[x] Received {message}");

                };

                //autoAck: indicar a la aplicación que envia que el mensaje ha sido recibido
                channel.BasicConsume(queue: queueName, autoAck: true,consumer:consumer);

                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
                #endregion

            }

        }
    }
}
