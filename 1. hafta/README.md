# Movie_Cohorts
Bu proje sipay .net bootcampi eğitimi süresince patika cohorts da ilk haftadaki ödev olan restful api projesidir.İlk hafta bizden restful apide istenilen maddelere bakalım.

### 1.HAFTA ÖDEVDE İSTENİLENLER
Restful Api Geliştirin
- Rest standartlarna uygun olmalıdır.
- GET,POST,PUT,DELETE,PATCH methodları kullanılmalıdır.
- Http status code standartlarına uyulmalıdır. Error Handler ile 500, 400, 404, 200, 201 hatalarının standart format ile verilmesi
- Modellerde zorunlu alanların kontrolü yapılmalıdır.
- Routing kullanılmalıdır.
- Model binding işlemleri hem body den hemde query den yapılacak şekilde örneklendirilmelidir. Bonus:
- Standart crud işlemlerine ek olarak, listeleme ve sıralama işlevleride eklenmelidir. Örn: /api/products/list?name=abc  c# dili kullanarak  .net teknolojisi ile geliştir.

Ödeve geçmeden önce Restful Api nedir bir ona bakalım böylece ne yaptığımzı daha iyi anlarız.
## RESTFUL API
<p> Öncelikle ‘API nedir, nasıl çalışır?’ tanımlayacak olursak; bir API veya uygulama programlama arabirimi (Application Programming Interface), uygulamaların veya cihazların birbirine nasıl bağlanabileceğini ve birbirleriyle iletişim kurabileceğini tanımlayan bir dizi kuraldır. API entegrasyonu, veri alışverişi yapmak ve ortak bir işlev gerçekleştirmek için API’leri aracılığıyla birbirine bağlanan ve böylece uygulamalar arasında etkileşimi sağlayan birkaç uygulamayı (iki veya daha fazla) ifade eder.

  REST, client-server arasındaki haberleşmeyi sağlayan HTTP protokolü üzerinden çalışan bir mimaridir. İstemci ve sunucu arasında XML ve JSON verilerini taşıyarak uygulamanın haberleşmesini sağlar. REST mimarisini kullanan servislere ise RESTful servis (RESTful API) denir.

Amazon, Google, Facebook, LinkedIn ve Twitter gibi çeşitli web siteleri, kullanıcıların bu bulut hizmetleriyle iletişim kurmasını sağlayan REST tabanlı API’leri kullanır.

REST ile yazılmış bir servisle çalışmak için ihtiyacımız olan tek şey URL. Bir URL’e istek attığımızda, URL size JSON veya XML formatında bir cevap döndürür, dönen cevap parse edilir ve servis entegrasyonunuz tamamlanır. Yani client uygulama, REST bir servisin yapısını ve detaylarını bilmek zorunda değildir. Rest servisler; client ve server arasındaki ayrım sayesinde, REST protokolü, bir projenin farklı alanlarındaki geliştirmelerin bağımsız olarak gerçekleşmesini kolaylaştırır. REST API, operasyonel sözdizimine ve platforma göre ayarlanabilir ve geliştirme sırasında çok sayıda ortamı test etme olanağı sunar.Kullanıcılar, REST client-server farklı sunucularda barındırılsa bile kolayca iletişim kurabilir , bu da yönetim açısından önemli bir fayda sağlar. </p>

<img align="center" src="https://cdn.hosting.com.tr/bilgi-bankasi/wp-content/uploads/2022/01/rest-api-nedir-nasil-calisir.jpg" alt="rest api çalışma mimarisi" width="500" height="400">



### ÖDEV
- İlk önce apide kullanıcağımız movie modelini oluşturalım.Bu model oluşturulurken de Data Annotaions kullanarak gerekli kuralları da ekledik. Şuan veritabanına bağlı değil projemiz.

```c#
   public class Movie
    {

        [Key]
        public int MovieId { get; set; }

        [Required]
        [StringLength(maximumLength: 50,MinimumLength =1)]
        public string MovieName { get; set; }
        public string  Category { get; set; }

        public int Duration { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        public string Director { get; set; }
    }
```
- Daha sonra Movie Controllerımızı oluşturduk ve üzerine Route tanımlaması yaptık ve ayrıca bunun bir api controller olduğunu söyleyen attribute 'ü ekledik.
  
 ```c#
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
    
    }
 ```c#

- Controller içinde static bir liste tanımlayıp direk içine değerleri ekledik.Projemizde veritabanı olmadığından statik bir liste kullandık. Statik liste kullandığımız için apiyi her çalıştırdığımızda ekldeiklerimiz değil bu haliyle gelicek.

 ```c#
private static List<Movie> MovieList = new List<Movie>()
        {

            new Movie()
            {
                MovieId=1,
                MovieName="Upgrade",
                Duration=100,
                Category="Science Fiction",
                Director="Leigh Whannell"

            },

            new Movie()
            {
                MovieId=2,
                MovieName="A Beautiful Mind",
                Duration=135,
                Category="Drama",
                Director="Ron Howard"

            },
            new Movie()
            {
                MovieId=3,
                MovieName="Inception",
                Duration=148,
                Category="Science Fiction",
                Director="Christopher Nolan"

            },

        };
```

- Daha sonra error handler için action metodu oluşturarak aldığı ststus code da göre mesaj veren bir error actionu oluşturduk.Bu [HttpGet] olarak gönderdik faat projede aynı şekilde birden fazla [HttpGet] kullanılmasına izin vermediği için [Route] attribute 'inden yararlanarak farklı bir route yönlendirmesi de ekledim.
  
 ```c#
public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            if (exception is NotFoundException)
            {
                return NotFound(new { message = exception.Message });
            }
            else if (exception is BadRequestException)
            {
                return BadRequest(new { message = exception.Message });
            }
            else
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }

        }

        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message)
            {

            }
        }

        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message)
            {

            }
        }
 ```

- Daha sonra bizden istenilen methodları kullanarak silme,ekleme,güncelleme,spesifik güncelleme,id ye göre listeleme,sıralama gibi işlemleri controllerıımızda yaptırdık. Burda örnek olması açısından Fromquery ile model bağla işlemi yaptığımız GetWithFromquery actionunu ve sıralama işlemini yaptığımız orderMovie actionunu örnek olarak koydum.Diğerlerini projenin içerisinden isterseniz bakabilirsiniz.

  ```c#
  //Fromquery ile yapılması
        [Route("fromquery/[controller]s")]
        [HttpGet]
        public IActionResult GetWithFromQuery([FromQuery] int id)
        {

            var movie = MovieList.Find(x => x.MovieId == id);

            //böyle bir film yoksa null gelicek ve bad request olarak response vericek
            if (movie == null)
            {
                return NotFound(new { message = "Movie not found" });
            }

            //değilse onu döndürecek
            return Ok(movie);
        }
  ```
  
Bu methodda Fromquery ile id değerini aldık.Daha sonra MovieList içerisinde bu id ye sahip bir film varmı yokmu diye kontrol işlemi yaptırdık.Eğer böyle bir id bulamassa NotFound döndürerek içerisinde mesaj yolladık. Zaten id yi bulmuşşsa o filmi geri döndürdük.

Burda ise filmleri ismine göre sıralayıp liste olarak döndürdüğümüz orderMovie methodunu görüyoruz.Burda filmleri linq kullanarak isme göre sıralayıp tolist medotu ile listeledik ve moviList değişkenine atadık. Ardından bu listeyi geri döndürdük.
  ```c#
//moviname göre sırslayıp getirme
        [Route("order/[controller]s")]
        [HttpGet]
        public List<Movie> orderMovie()
        {
            var movielist = MovieList.OrderBy(x => x.MovieName).ToList();
            return movielist;
        }
  ```
