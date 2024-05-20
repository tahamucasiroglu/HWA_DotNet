# Kaynak Listesi

* [sefikcankanber_Medium](https://sefikcankanber.medium.com/asp-net-core-cache-kullan%C4%B1mlar%C4%B1-in-memory-cache-kullan%C4%B1m%C4%B1-34d54d91d3ce)

* [GençayYıldızBlog](https://www.gencayyildiz.com/blog/asp-net-corede-in-memory-cache/)



# Neden

Sürekli erişilen fakat nadiren değişmesi gereken verileri `memory cache` ile hazırda tutarız. Taze okumaktayım fakat basit bir örnek verirsek; <br>
her istekte tarih vereceğiz diyelim. Sürekli `Datetime.Now` yorar demek yorar. (Yorar diye düşünelim.) Benim saat ile de işim yok ise. Ben her gün `00:00`'da güncellenecek şekilde memory cache oluştururum ve bunu kullanırım. Sürekli sistemden saat çekmektense memorydeki veri kullanılır gibi gibi. Örnek saçma geldi ise şimdi daha saçması olan koduna geçelim.

# Kurulum
İçinde geliyormuş ek pakete gerek yokmuş

### Program.cs

`var app = builder.Build();` kısmının hemen üstüne `builder.Services.AddMemoryCache();` memory cache ekleyelim. Galiba App içinde aktif etmeye gerek yok.


### Controller.cs

```
private readonly IMemoryCache _memoryCache;

public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache memoryCache)
{
    _logger = logger;
    _memoryCache = memoryCache;
}
```

öncelikle başlangıç kurulumunu ekleyeyil.

```
[HttpGet(Name = "GetWeatherForecast")]
public IEnumerable<WeatherForecast> Get()
{
    string cacheKey = "Tarih";

    if (!_memoryCache.TryGetValue(cacheKey, out DateTime dateTime))
    {
        dateTime = DateTime.Now;
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };
        _memoryCache.Set(cacheKey, dateTime, cacheEntryOptions);
    }
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        ResponseDateReal = DateTime.Now,
        ResponseDateCache = dateTime,
        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();

}
```
get kodunu ise yukarıdaki gibi yaptık. 

```
{
    "responseDateCache": "2024-05-20T11:53:27.5050029+03:00",
    "responseDateReal": "2024-05-20T11:55:08.8247139+03:00",
    "date": "2024-05-21",
    "temperatureC": 35,
    "temperatureF": 94,
    "summary": "Mild"
}
```

örnek çıktı ise görseldeki gibi olacak. 

### Açıklama 

`if (!_memoryCache.TryGetValue(cacheKey, out DateTime dateTime))` Burada eğer `datetime` değeri dolu ise verecek ve if dışında kullanarak işlerimizi yapacağız eğer dolu değil ise önce `dateTime = DateTime.Now;` ile değeri biz vereceğiz. Sonrasında `options` açarak ayarları yapacağız. <br>
`AbsoluteExpirationRelativeToNow` özelliği verinin tutulma süresini verir. burada 5 dakika dediğimiz için 5 dakika sonunda otomatik olan veride silinir bu sayede tekrar veri dolumu olur. <br>
`TimeSpan.FromMinutes(2)` ise burada 2 dk boyunca erişilmez ise otomatik sil demek. ister süre bitsin ister bitmesin. 2 dk boyunca burada aktivite olmaz ise reset atılır.

<br>

burada örnek olarak böyle yaptım tahminimce bu yapının ayrı bir katmanda ayarlanması lazım çünkü dağınık olarak tutulabilecek bir durum değil. Toplu reset vs gerekse zor olacak gibi.



















