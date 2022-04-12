# .NetCore Identity Register NOTLARI

## Register İşlemi
Register işleminde Identity içerisinde bulunan çoğu property kullanılmayacağı için ViewModel içerisinde RegisterVM modeli oluşturuldu.</br>
Kullanıcı kayıt işlemlerini yapabilmemiz için Startup içerisindeki ConfigureServices ‘da bunu belirtmemiz gerek.</br>


### Startup ConfigureServices
```csharp
services.AddIdentity<IdentityUser, IdentityRole>(x =>
{
       x.Password.RequireNonAlphanumeric=false;
       x.Password.RequireDigit=false;
       x.Password.RequireLowercase=false;
       x.Password.RequireUppercase=false; 

}).AddErrorDescriber<CustomValidation>() //CustomValidation adında tanımladığımız class içerisindeki Custom Hata mesajlarını dahil eder.
.AddEntityFrameworkStores<AuthContext>(); //Hangi veritabanını kullanıyor bunu belirtir.

```

Kullanıcı inputlarında varsayılan olarak belirlenen bazı kısıtlamalar vardır. Password 6 karakter olacak vs. bunlar AddIdentity metodu içerisinde restore edilebilir. Ancak bu kısıtlamaların hata mesajları varsayılan olarak ingilice döner. Bunları custom olarak belirleyebilmek için;</br>

•	Tools klasörü oluşturulur.</br>
•	Klasör içerisine CustomValidation isminde class oluşturulur.</br>
•	Bu class IdentityErrorDescriber classından miras alır.</br>
•	Bu class içerisinde virtual olarak tanımlanmış hata mesajlarını barındıran metotlar vardır. Bunları override ederk değiştirebiliriz.</br>
•	Bu class İçerisinde aşağıdaki örnekteki gibi işlemler gerçekleştirildikten sonra AddIdentity metoduna AddErrorDescriber ile yukarıdaki gibi eklenir.</br>

### CustomValidation Classı
```csharp
public class CustomValidation : IdentityErrorDescriber
{
    public override IdentityError PasswordTooShort(int length)
    {
        IdentityError error = new IdentityError();
        error.Code = "PasswordToShort";
        error.Description = $"şifreniz en az {length} karakter olmalı!";
        return error;
    }
```
### Register Action ı
Action içinde kullanıcı oluşturma işlemi için öncelikle ilgili Controllerın constructor ‘ına UserManager<IdentityUser> userManager ve readonly field ı eklenmelidir.</br>

userManager içerisindeki Create metodu Asenkron bir metotdur bunun için ilgili kod await keywordü ile, ilgili metotda async ve Task ile işaretlenmelidir.</br>

Bu metot geriye bir sonuç döner. Bu sonuç başarılı ise “succeeded” değil ise hata mesajı koleksiyonu döner.</br>

Bu hata mesajları içerisinde dönerek AddModelError ile hata mesajlarını ModelState e ekleyebiliriz.</br>

```csharp
[HttpPost]
public async Task<IActionResult> Register(RegisterVM registerVM)
{
    if (ModelState.IsValid)
    {
        IdentityUser user = new IdentityUser();
        user.Email = registerVM.Email;
        user.UserName = registerVM.Email;
        var result = await userManager.CreateAsync(user, registerVM.Password);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(err.Code, err.Description);
            }
            return View(registerVM);
        }
    }
    else
    {
        return View(registerVM);
    }
}


```
