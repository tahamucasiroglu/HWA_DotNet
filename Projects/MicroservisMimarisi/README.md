[KAYNAK Gençay Yıldız](https://www.youtube.com/c/Gen%C3%A7ayY%C4%B1ld%C4%B1z)

<br>

<br>

<br>

# Mikroservis Mimarisi Nedir?

Bir yaklaşımdır. Bağımsız ve ölçeklendirilebilir. Tek bir işe ayrılmış. Küçük hizmetlere ayırarak ve dağıtılarak geliştirilmesidir. 

<br>

## Dağıtabilirlik Nedir?

farklı özellikli makinalarda çalışması dağıtabilirliktir. Programı farklı sunuculara bölerek çalışabilmesidir. 


## Ölçeklenebilirlik

Artan işlem gücüne karşı performansı koruyarak devam etmesine sürdürmektir. Servisi çoğaltarak paralel olarak daha hızlı görevleri tamamlamaktır.

### Scale Out

iş yükünü birden fazla aynı özellikli sunuculara dağıtılmasıdır.

### Scale Up

iş yükü fazla geldiğinde sunucunun özelliklerini yükseltilmesidir. 



## Temel Prensipler

* Bağımsızlık = Her servis birbirinden bağımsız olacak. Her servis kendi veritabanını ele alacak. 
* Teknoloji Çeşitliliği = Servisler farklı dil ve teknolojiler ile geliştirilbilir. 
* İşlev Bölünme = Her şey bölüneceği için her servis işine odaklı olacağından yönetim daha kolay olacak.
* Hafif İletişim = servisler kendi arasında ihtiyaç doğrultusunda iletişim kurabilir. Bu kısımda rabbitmq kullanılabilir. Genelde asenkron iletişim olacağı için gevşek bağlılık ile servisler arası bağımlılıklar olmayacak gelişime etkisi olmayacaktır.
* Ölçeklendirme = her servis ihtiyaca göre ölçeklendirilebilecek.
* yüksek kusur toleransı = bir servisin çökmesi bozulması diğer servisleri etkilememeli


<br>

mikro servislerin mantığı büyük ve karmaşık uygulamaların küçük parçalar halinde kolay anlaşılır şekilde yönetilmesini sağlar. 


<br>


## Organizasyon Modelleri

### Teknoloji Odaklı

Her servis farklı teknolojide geliştirilir. Geliştiriciler için çeşitlilik olur. 


### İş Odaklı

ekipler servislerini iş bitirecek şekilde yaparsın. Kullanıcı servisi bir ekipte olur gibi. 

### Veri odaklı

müşteri verisi servisi gibi servisler ile iş çözülür. verilerin temelde olduğu servisler olur. 


## Api Gateway

servisler ile clientlar arasında iletişimi kolaylaştırır. 
























