# WEBAPI Basic Authorization NOTLARI

Filter klasörü altında BasicAuth isminde bir class oluşturuldu.<br>
Bu class AuthorizationFilterAttribute classından miras alacak ve onAuthorization metodu overide edilecek.<br>
Filtrelenecek Controllerın veya actionın üzerine [BasicAuth] attribute u eklendi.<br>
Login işlemini databaseden kontrol edecek metot tanımlamak için Credentials adında bir klasör oluşturup içerisinde LoginCredential classı ve içerisinde static Login isminde metot oluşturuldu.<br>
### BasicAuth Class
```csharp
public class BasicAuth:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //gelen istek ile beraber kullanıcı adı ve şifre yazılmamışsa ziyaretçiye tekrar "unauthorized" kodunu mesaj ile beraber döndürüyoruz.
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized,"bu alanı görüntülemeye yetkiniz bulunmamaktadı!");
            }
            else
            {
                //gelen istek içerisinde kullanıcı adı ve şifre yazılmışsa bu bilgiler bize her zaman Base64 şeklinde şifrelenmiş olarak teslim edilir. Bu durumda bu şifreyi string (Utf8) olarak çevirmemiz ardından veritabanında kontrol etmemiz gerekmektedir.
                string authToken = actionContext.Request.Headers.Authorization.Parameter;

                string decodeAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                string[] usernamePasswordArray = decodeAuthToken.Split(':');
                string userName = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                if (LoginCredential.Login(userName, password))
                {
                    //Principal
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "bu alanı görüntülemeye yetkiniz bulunmamaktadı!");
                }
            }
        }
    }
```
### LoginCredential Class
```csharp
public class LoginCredential
    {
        public static bool Login(string _username, string _password)
        {
            NorthwindEntities db = new NorthwindEntities();
            bool result = db.Users.Any(x => x.Username.Equals(_username) && x.Password.Equals(_password));
            return result;
        }
    }
```
### JQuery Code
```jquery
$.ajax({
            type: 'Get',
            url: '../api/products',
            dataType: 'json',
            headers: {
                "Authorization": "Basic " + btoa(username + ':' + password)
            },
            success: function (data) {
                GetProducts(data);
            }
            }
        })
```
### Kod Açıklamaları

```csharp
    actionContext.Request.Headers.Authorization 
```
Client dan gelen username ve şifreyi jquery code ile headers Authorization a gönderdiğimiz için bu bilgiler base64 olarak bu alana düşer. Bu kodun beraberinde .Parameter ile parametresi çekilebilir.
```csharp
    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized,"bu alanı görüntülemeye yetkiniz bulunmamaktadı!");  
```
Bir response/ cevap oluşturulur.
```csharp
    Encoding.UTF8.GetString(Convert.FromBase64String(authToken)); 
```
parametresine verdiğimiz base64 tipli yapıyı UTF8 e çevirir string döner. (Username:password)
```csharp
    string[] usernamePasswordArray = decodeAuthToken.Split(':');
    string userName = usernamePasswordArray[0];
    string password = usernamePasswordArray[1];
```
username ve password ayrılır.
```csharp
    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
```
Geçerli izine yeni bir izin tayin eder. Null verilen değer role bölümüdür. Kullanıcı ismi ve şifresi doğru ise bu işlem gerçekleştirilir.
```jquery
    headers: { 
        "Authorization": "Basic " + btoa(username + ':' + password)
       }
```
Jquery ajax içerisinde headers optionı kullanılmalıdır. acrtionContexte username ve passwordu base64 türüyle gönderir.
