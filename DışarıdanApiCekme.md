# Dışarıdan Api Çekme NOTLARI

Dışarıdan çekilen veriyi class yapısına uydurup bu class yapısı üzerinden işlem sağlamak daha sağlıklı bir işlem olacaktır.<br>
Öncelikle İlgili verinin propertylerini barındıracak bir model classı oluşturulur.<br>
Utils isimli klasör içerisinde veriyi çekip json tipini kullanarak oluşturduğumuz class tipinde liste dönecek ve urlyi parametre olarak alacak ConvertJsonToClass isminde bir class tanımlanır.<br>
Bu class içerisinde ConvertJsonFrom isminde static bir metot tanımlanır.<br>

### ConvertJsonToClass Class
```csharp
public class ConvertJsonToClass<T> where T: class
    {
        public static List<T> ConvertJsonFrom(string url)
        {
            //herhangi bir web sitesine istekte bulunmak için aşağıdaki nesneyi kullanmamız gerekmektedir.
            WebClient client = new WebClient();

            //parametreden alınan url'i yukarıda almış olduğumuz instance ile istekte bulunup bütün veriyi string oalrak yakalıyoruz.
            string jsonFormat = client.DownloadString(url);

            //string olarak aldığımız verileri (jsonFormat) Covid19 isimli class içerisinde bulunan property'lere taşıyoruz. (Deserialize)
            var jsonClass = JsonConvert.DeserializeObject<List<T>>(jsonFormat);

            return jsonClass.ToList();
        }
    }
```
### xxController - GetAction
```csharp
public IHttpActionResult Get()
        {
            //https://api.covid19api.com/dayone/country/turkey

          var covidList=  ConvertJsonToClass<Covid19>.ConvertJsonFrom("https://api.covid19api.com/dayone/country/turkey");
            return Ok(covidList);
        }
```
