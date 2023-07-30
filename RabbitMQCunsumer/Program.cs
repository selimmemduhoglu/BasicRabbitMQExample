//Bağlantı oluşturma.
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://cnmpgfqs:SZumam1-KBFE-038pLfxyATAhTQji5Vj@toad.rmq.cloudamqp.com/cnmpgfqs"); //amqp üzerinden cloud ile bağlantı oluşturduk.


//Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma.
channel.QueueDeclare(queue: "example-queue", exclusive: false);
//Publisher da nasıl bir kuyruk oluşturduysak birebir aynı kuyruğu tekrardan Consumer da yani burada oluşturmamız gerekecek aynı kuyrukla senkron olması için

//Queue'dan mesaj okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);
//ne zamanki yukarada oluşturduğumuz kuyruğa mesaj gelirse sen bunu consume(tüket-kullan) et demiş oluyoruz.

consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen mesajın işlendiği yerdir.
    //e.Body : Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    //e.Body.Span veya e.Body.ToArray(); =>> kuyruktaki mesajın byte verisini getirecektir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));// burada da elde ettiğimiz byte türünü stringe çevirdik.
};

Console.Read();

//Bu örnek en basit haliyle publisher ve consumer u işleyerek mesaj gönderip mesajı okuduk.
//mesajı okuduktan sonra da mesajı siliyor.