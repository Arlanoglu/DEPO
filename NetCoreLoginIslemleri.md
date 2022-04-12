# .NetCore Identity Login NOTLARI

## Login İşlemi
Login işleminde de Register işleminde olduğu gibi Identity içerisinde bulunan çoğu property kullanılmayacağı için ViewModel içerisinde LoginVM modeli oluşturuldu.</br>
İlgili Controller’ın constructor ‘ına login işleminde kullanılacak SignInManager eklenir.</br>
signInManager içerisindeki FindByEmailAsync ve PasswordSignInAsync metodu Asenkron bir metotdur bunun için ilgili kod await keywordü ile, ilgili metotda async ve Task ile işaretlenmelidir.</br>

“PasswordSignInAsyn”metotu geriye bir sonuç döner. Bu sonuç başarılı ise “succeeded” olacaktır. Buna nazaran başarılı değil ise ModelState e AddModelError ile hata döndürülebilir.</br>



### Login Actionu;

Buradaki FindByEmailAsync metodu email e göre kullanıcıyı bulacak,</br>
PasswordSignInAsync ise kullanıcı ve şifreye göre bir kontrol sağlayacak.</br>

loginVM.RememberMe giriş bilgilerinin hatırlanıp hatırlanmaması için true false değeridir.</br>

False ise hatalı giriş yapıldıysa hatalı giriş sayılsın mı ifade etmektedir.</br>

```csharp
[HttpPost]
public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("failedLogin", "şifre hatalı");
                    }

                }
                else
                {
                    ModelState.AddModelError("notfound", "böyle bir email kayıtlı değil!");
                }


            }
            return View();
        }


```