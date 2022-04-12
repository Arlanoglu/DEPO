# .NetCore Cookie NOTLARI

## Cookie İşlemi

•	Authentication : Kimlik yönetimidir. SignInManager ile giriş yapma.</br>
•	Authorization : Yetki yönetimidir. Cookie ile yetki verme.</br>

Öncelikle login yapma işlemi ve giriş yapan kulanıcıya yetki verme işlemi için Startup içindeki Configure;	</br>

```csharp
    app.UseAuthentication(); //kimlik yönetimi
    app.UseAuthorization(); //yetki yönetimi

```
Eklenmelidir. Buradaki sıralama önemlidir. Kimlik yönetimi önce olmalıdır.</br>
Yetkisiz giriş yapılmasını istemediğimiz action veya controller üzerine Authorize tanımlanmalıdır.</br>
Mvc de web.config içerisinde tanımladığımız cookie işlemini NetCore da startup içerisinde gerçekleştiriyoruz.</br>

Öncelikle Startup içerisindeki servislere Cookie eklenir.</br>
Burdaki cookie tanımlamaları bunlarla sınırlı değildir.</br>

### Startup ConfigureServices;
```csharp
    services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = new PathString("/Home/Login");
                x.AccessDeniedPath = new PathString("/Home/Denied");
                x.Cookie = new CookieBuilder
                {
                    Name = "dotnetAuthCookie"
                };
                x.SlidingExpiration = true;
                x.ExpireTimeSpan = TimeSpan.FromMinutes(1);
            });


```

•	LoginPath : Bir authorize işleminde yönlendirilecek login yolunu belirler.</br>

•	AccessDeniedPath : Yetkisiz bir giriş yapılırsa yönlendirilecek yetkisi yok sayfasının yolunu belirler.</br>

•	SlidingExpiration : Cookie nin bir ömrü olup olmayacağını belirler.</br>

•	ExpireTimeSpan : Ömrünün ne kadar olacağını belirler.</br>