using System.Text;
using RabbitMQ.Client;

namespace Send;
class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                #region "envio de mensajes SIN exchange"
                // //crear o declara la cola
                // channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                // //crear el mensaje
                // string message = "Hola mundo!";
                // //pasamos a través de la cola los btyes del mensaje
                // var body = Encoding.UTF8.GetBytes(message);

                // //publicamos el mensaje
                // channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);

                // Console.WriteLine($"[x] Sent {message}");
                #endregion

                #region "envio de mensajes CON exchange"

                //declaramos el exchange
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                //crear el mensaje
                string message = GetMessage(args);
                //pasamos a través de la cola los btyes del mensaje
                var body = Encoding.UTF8.GetBytes(message);

                //publicamos el mensaje
                channel.BasicPublish(exchange: "logs", routingKey: "", basicProperties: null, body: body);

                Console.WriteLine($"[x] Sent {message}");
                #endregion
            }

            Console.WriteLine("Press any key to exit ...");
            Console.ReadLine();
        }



    }

    private static string GetMessage(string[] args)
    {
        return (args.Length > 0 ? string.Join(" ", args) : "info:Hola Mundo!");
    }
}
