
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://cnmpgfqs:SZumam1-KBFE-038pLfxyATAhTQji5Vj@toad.rmq.cloudamqp.com/cnmpgfqs"); //amqp üzerinden cloud ile bağlantı oluşturduk.


//Bağlantıyı Aktifleştirme ve kanal açma. //Idisposible patern kullandık.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma.
//exclusive: false, olmasının sebebi publisher oluşturduktan sonra başka bir customer in bağlanabilmesi için özelleştirmeyi kaldırdık.
channel.QueueDeclare(queue: "example-queue", exclusive: false);


//Queue ya Mesaj Gönderme.
//RabbitMQ kuyruğa atacağı mesajları byte türünden kabul etmektedir. Haliyle mesajları bizim byte a dönüştürmemiz gerekecektir.
byte[] message = Encoding.UTF8.GetBytes("Merhaba");
channel.BasicPublish(exchange: "", routingKey: "example-queue",body: message);
//exchange i bu örnekte boş geçtik default olarak zaten direkt ezchange i tercih edilecek.
//Routing Key kuyruğun ismidir.

Console.Read();