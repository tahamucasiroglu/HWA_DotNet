# gRPC  (evet g küçük kalanı büyük)


Google tarafından geliştirilen open source `remote procedure call(RPC)` sistemidir. `RPC` sistemi kısaca kendi uygulamalarında uzaktan gönderdiğin bir kodu gönderdiğin yerde çalıştırmandır. 

<br>

Http/2 ile iletişim yapar. 

![alt text](image.png)
[KAYNAK](https://www.youtube.com/watch?v=FFqg-WhhOw4&list=PLQVXoXFVVtp3oS21qi7a0DZikNPAWxevZ)

<br>

http/1 her şey için ayrı istek atar. http/2 ise tek istekte hepsini çözer. [Kısaca n to 1 mux donanımının yazılım hali](https://tr.wikipedia.org/wiki/%C3%87oklay%C4%B1c%C4%B1)

<br>

hızlı kullanışlı bu sebep ile tercih edilir. 

### unary iletişim

tek istek ve tek cevap alınan iletişim türüdür.


### Server Streaming

tek istek karşılığında stream cevap alınmasıdır. yani istek atılır ve çoklu cevap alınır. 

### Client Streaming

Tersidir birden çok istek karşılığında tek cevap olur.

### Bi-Directional Streaming

iki tarafında karşılıklı birden çok istek ve cevap ürettiği türdür. Karşılıklı konuşmadır direkt.


### prote dosyası

client ve server arası iletişimi sağalr.

# Uygulama

` dotnet new grpc --name grpcServer` ile otomatik grpc server oluşturulur

<br>

![alt text](image-1.png)


burada protos içinde `proto` tanımları yapılır. services içinde ise proto sırasında kullanılacak kalıplar yer alır

<br>

![alt text](image-2.png)
proto dosyası

<br>

syntax versiyon gibidir. 3. satırdaki ise namespace belkirtit.

<br>

|10. satırda  rpc yanındaki request edilecek tr return yanındaki ise response edilecek türdür. 

<br>

|15. ve 20. satırdaki 1 değerleri döndürme sırasıdır.

<br>

![alt text](image-3.png)
client projesine üç projeyide kur.

<br>

server kısmındaki proto dosyasını client içine kopyala. proto birebir aynı olmalı. klasör olarak değil sadece dosya



<br>


![alt text](image-4.png)
10-11-12 satırlarındaki kodu kendi proje dosyanıza ekleyin client proje dosyası

<br>

sonra build edin


<br>

![alt text](image-5.png)


client kısmında program.cs dosyası

<br>

![alt text](image-6.png)


sonuç bu şekilde

<br>

gençay yaptı bende server niye yazmıyor diye 1 saat düşündükten sonra 

![alt text](image-7.png)


burada 16. satırdaki kodu girmeyi unutmayın bunuda server kısmında services içindeki dosyaya ekleyeceksiniz
<br>
temel grpc yapısı taammalandı

# Özel Protolar yapma







