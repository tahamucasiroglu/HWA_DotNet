# Kaynak

* [Gençay Yıldız A'dan Z'ye RabbitMQ Eğitimi](https://www.youtube.com/watch?v=3SHFwb7exgM&list=PLQVXoXFVVtp2aVwD6GX2KCjcD3hSe6vWM)

### Message Queue nedir

yazılım sistemlerinde iletişim için kullanılır. Gönderilen mesajları kuyrukta saklar ve zamanı gelince iletir. Mesaj gönderene `publisher || producer` denmekte alıcı ise `Consumer` denmekte. 

<br>

`Message queue` asenkron şeklinde işler. İşler yapılır sonrasında MessageQ işleri yapılır.

<br>

burada asenkron derken e-ticarette sipariş verdikten 1 saat yada 1 gün sonra e-fatura gelmekte. Burası `MessageQ` örneği için ideal. Sipariş tamam kargo tamam vakti gelince faturada tamamlanır.

<br>

### Message Broker

içerisinde MessageQueue barındıran ve bu sistem içinde alıcı verici ilişkisini sağlayan sistem. Bir messageBroker içinde birden fazla message queue bulunabilir. 

bu teknolojileri kullananlardan bazıalrı

* RabbitMQ 
* Kafka
* ActiveMQ
* ZeroMQ
* NSQ
* IronMQ
* Redis

anlık cevap verilmesine gerek olmayan, uzun süreli çalışan ve anlık olmayan işlerde `RabbitMQ` kullanırız. Asenkron süreci kontrol eder. 

<br>

![alt text](image.png)
[Kaynak](https://youtu.be/3SHFwb7exgM?si=EaJzIbrhY7Fha3rJ&t=1948)

Publisher ile Customer aynı dil yapı olmasına gerek yok iletişimi sağlasın yeter.

<br>

### Docker Kurulumu

iki şekilde RabbitMQ seçeniği var docker ile kullanabilirsin yada cloud kullanabilirsin. Eğitim cloud ile devam edecekmiş. docker bilgi için.

<br>

7 Haziran 2024 tarihli kurulum komutu `docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.13-management` bunu `CMD` üzerinden verdiğinizde kurulum başlar. Komut için [https://www.rabbitmq.com/docs/download](https://www.rabbitmq.com/docs/download) 

<br>

Sonrasında [http://localhost:15672/](http://localhost:15672/) adresine gidersen orada `RabbitMQ` giriş sayfasını göreceksin. burada container ile rabbitmq kullanmış olduk.

<br>

![alt text](image-1.png)

<br>

Username ve Şifre için ilk değer `guest` dir. Yani <br>
Username = guest<br>
Password = guest<br>


![alt text](image-2.png)


gelen ekranda bu şekilde olur.

<br>

### Cloud Kurulumu

[https://www.cloudamqp.com/](https://www.cloudamqp.com/) adresinden login olun. Google hesabı ile olma var fakat ileride dikkat edin reklam mailleri diye sormakta orada seçimi yapın. Team name vs zorluyor onları geçtikten sonra Create new instance diyin.

![alt text](image-3.png)


<br>

![alt text](image-4.png)

devam diyoruz.


![alt text](image-5.png)


![alt text](image-6.png)

![alt text](image-7.png)

rabbitmq manager tıkla. burası çok yavaş ama bedava sonuçta. <br><br> şimdi burada birde direkt isime yani burada `TahaExample` kısmına tıklayın.  

<br>

resmi koyamam güvenlik için burada da bağlantı bilgileri adres bilgileri filan var.

<br>

### Genel Yapı

![alt text](image-8.png)

<br>

exchange = publisher tarafından gelen mesajlara ne yapacağına karar veren yapıdır. birden fazla kuyruk olduğunda hangi kuyruğa gideceğini filan ayıklar. değnekçi diyebiliriz.

<br>

routes = mesajların exchange üzerinden kuyruklara nasıl gideceğini belirler. bazen route olmayabilir. yöntem derken ise header gibi düşünebiliriz. 


<br>

### Binding

![alt text](image-10.png)

exchange ile queue lar arası bağ binding dir. 

### Exchange Types

#### Direct Exchange

sadedir. mesajlar direkt olarak kuyruğa gider. 

#### Fanout Exchange

route key message key ayırt etmeden mesajı tüm kuyruklara atılır. en ilkelidir denebilir.

#### Topic Exchange

routing key ile kuyrukalra mesaj iletir. 

![alt text](image-11.png)
[`gençay yıldız resmi`](https://youtu.be/vBv7FbmInqM?si=MD4v74iSb2td_R_c&t=1111)

#### Header Exchange

routing yerine header kullanır. 

![alt text](image-12.png)
[`gençay yıldız resmi`](https://youtu.be/vBv7FbmInqM?si=MD4v74iSb2td_R_c&t=1111)


### .NET kullanımı

Bu aşamada `RabbitMQExampleConsole` üzerinden yapacağım kodlar orada.
<br>

RabbitMQ.Client Kütüphanesini kur.

<br>

önce publisher üretilir. öncesinde rabbitmq bağlantısı hazırlanmalı. cloud bağlantısı olacak docker ile de olabilir. 

<br>

instance üzerine tıklayınca gelen ekrandan bilgilerle gireceğiz. 

![alt text](image-13.png)
o kadar boyadım özeldir diye adresleri kodda vereceğim üşendim her yerden gizlemeye silerim instance ı olur biter.

<pre><code class='language-cs'>
//bağlantı kısmı
ConnectionFactory factory = new();
factory.Uri = new(String_Uri_Here); 

//bağlantı açıp aktif etme
using IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

// queue oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);

// queue mesaj gönderme
// rabbitmq queue atacağı mesajları byte türünde atar. mesajlar byte türünde kullanıulır.
byte[] message = Encoding.UTF8.GetBytes("Merhaba");

// exchange boş verirsen rabbitmq da default exchange davranışına girer ve direct exchange gibi davranır.
channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

Console.Read();
</code></pre>

resimdeki url kısmını buradaki factory.Uri kısmındaki string değere vermekteyiz. Sonrasında queue ve yayıncı ve alıcı ayarlamaları olacak. şuan bu kodu çalıştırdığımızda rabbitmq üzerinde manager kısmında connections kısmına bakarsak bir adet bağlantı olduğu anlaşılacka.

![alt text](image-14.png)


<br>

Alıcı kısmına geçiyoruz.

<br>

<pre><code class='language-cs'>
ConnectionFactory factory = new();
factory.Uri = new("amqps://mcqcpoqg:8A8S5zibDUxxgWsQlJUuz4xHgi0atEk6@chimpanzee.rmq.cloudamqp.com/mcqcpoqg"); //bağlantı kısmı
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// queue oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};
</code></pre>

farklı proje açmaya üşendim ondan tekte yapmaya çalıştım kodlar bu şekilde ama paralel test edemedim. muhtemelen farklı proje ile yapmam gerekecek.

### Gelişmiş Kuyruk Teknoloji

* Eound-Robin Dispatching = mesajlar alıcılara sırayla iletilir. 1-2-3-4-5-6 diye mesaj var diyelim sıralı. 3 alıcı varsa 1-4, 2-5, 3-6 şeklinde alırlar. 1 mesaj verir diğerine geçer sonra başa döner.

* Message Acknowledgement = mesaj iletilince mesaj kuyruktan silinir. fakat iletilen mesajın işlenmemesi durumundada silinir bunun için hemen silmeyiz. işleme sonrası başarı ile ilendi mesajı gönderip o zaman silinmesini sağlarız. yani doğrulayarak silme yapmaktayız. bu yönteme verilen isim.

* BasicNack ile işlenmeyen mesajları 

* kalanları yazmadım bir kaç durum daha var

### Direct Exchange

kuyruk adı ile göndermekt yetmekte bunda

<pre><code class="language-cs">
private static void DirectExchangePublisher()
{
    ConnectionFactory factory = new();
    factory.Uri = new("amqps://mcqcpoqg:8A8S5zibDUxxgWsQlJUuz4xHgi0atEk6@chimpanzee.rmq.cloudamqp.com/mcqcpoqg"); //bağlantı kısmı
    IConnection connection = factory.CreateConnection();
    IModel channel = connection.CreateModel();

    channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

    for (int i = 0; i < 100; i++)
    {
        Console.Write("Mesaj : ");
        byte[] message = Encoding.UTF8.GetBytes($"Mesaj {i}");
        channel.BasicPublish(exchange: "direct-exchange-example", routingKey: "direct-queue-example", body: message);
    }
}

private static void DirectExchangeConsumer()
{
    ConnectionFactory factory = new();
    factory.Uri = new("amqps://mcqcpoqg:8A8S5zibDUxxgWsQlJUuz4xHgi0atEk6@chimpanzee.rmq.cloudamqp.com/mcqcpoqg"); //bağlantı kısmı
    using IConnection connection = factory.CreateConnection();
    using IModel channel = connection.CreateModel();

    channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

    string queueName = channel.QueueDeclare().QueueName;

    channel.QueueBind(queue: queueName, exchange: "direct-exchange-example", routingKey: "direct-queue-example");

    EventingBasicConsumer consumer = new(channel);
    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

    consumer.Received += (sender, e) =>
    {
        string message = Encoding.UTF8.GetString(e.Body.Span);
        Console.WriteLine(message);
    };

}
</code></pre>

özünde `exchange` içinde tip olarak direct diye belirtiyorsun ek olarak `queue` içinde de `routingKey` belirtiyorsun. Rabbitmq da çalışma sırası routingkey kısmı ile eşleşen tüm consumerlara mesajı iletecektir.

### Fanout Exchange

[KAYNAK](https://github.dev/gncyyldz/RabbitMQ-Tutorials)
```
publisher
channel.ExchangeDeclare(
    exchange: "fanout-exchange-example",
    type: ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");

    channel.BasicPublish(
        exchange: "fanout-exchange-example",
        routingKey: string.Empty,
        body: message);
}
```

```
consumer
channel.ExchangeDeclare(
    exchange: "fanout-exchange-example",
    type: ExchangeType.Fanout);

Console.Write("Kuyruk adını giriniz : ");
string queueName = Console.ReadLine();

channel.QueueDeclare(
    queue: queueName,
    exclusive: false);

channel.QueueBind(
    queue: queueName,
    exchange: "fanout-exchange-example",
    routingKey: string.Empty
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};
```


Direct exchange ile tip belirleme ve routingkey farkı var. Routingkey boş olma sebebi burayı dinleyen tüm kuyruklara göndereceğimiz için. Zaten bir tane kuyruk istersen direckt kullanacaksın hepsi içinde fanout kullancaksın. yanlış kavramadıysam. ilgili exchange içindeki kuyruklar sadece tüm kuyrklar değil.




### Topic Exchange

1 ay uzak kaldım bunu burada sonra dönmek için bırakıyorum.
















