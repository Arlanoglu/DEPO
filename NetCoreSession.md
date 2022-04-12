# .NetCore Session NOTLARI

## Session İşlemi
NetCore çekirdek bir yapı olmasından kaynaklı tüm sistemlerde ortak olarak karşılanabilmesi için Session lar string değer alabilmektedirler. </br>

Session oluşturmak için SET , çağırmak için GET metotları kullanılır. Ancak bu metotlar byte dizisi tipinde veri almaktadırlar. </br>

Byte dizileri ile işlem yapmak yerine obje türü ile işlem yapabilmek için opsiyonel olarak custom Extension metotlar oluşturulabilir. Bu metotlar yine obje ile işlem yapacak ama döndürdüğü değerler json tipinde olacaktır.</br>

Bunun için öncelikle projeye “NewtonSoft.Json” kütüphanesi kurulmalıdır.</br>



### Gerekli işlemler;


1)	CustomExtensions isminde bir klasör oluşturuldu.</br>
2)	Bu klasör içerisinde CustomSessionExtension isminde static bir class oluşturuldu.</br>
3)	Extension metot oluşturmak için ilgili class ve metotlarının static olması gerekir.</br>
4)	Classs içerisinde Session ı oluşturacak SetObject metodu ve Sessionı çağıracak GetObject metodu oluşturuldu. </br>
5)	HttpContext.Session , Isession tipinde olduğu için metot parametre olarak Isession alacaktır. Ancak burada önemli olan nokta this keyword ünün kullanılmasıdır. Metodun HttpContext.Session dan sonra . ile ulaşılabilmesi için bu keyword kullanılmalıdır.</br>
6)	Session ı kullanabilmek için Startup service lere eklememiz gerekir ve bu service i kullanabilmek için configure tarafında kullanılacağı belirtilir.</br>

```csharp
//ConfigureServices
services.AddSession(x =>
            {
                x.Cookie.Name = "product.Session";
                x.IdleTimeout = TimeSpan.FromMinutes(5);
            });
//Configure
app.UseSession();//session kullanmak için tanımlamalıyız.

```

7)	Gerekli işlemler;</br>

```csharp
public static class CustomSessionExtension
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string stringData = JsonConvert.SerializeObject(value);
            session.SetString(key, stringData);
        }
        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            var jsonData = session.GetString(key);
            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            return null;
        }
    }

```