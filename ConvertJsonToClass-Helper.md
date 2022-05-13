# ConvertXmlToClass - Helper

### ConvertXmlToClass - Class
```csharp
public static T ConvertXmlFrom(string url)
        {
            try
            {
                WebClient client1 = new WebClient();
                var xmlFormat = client1.DownloadString(url);


                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(xmlFormat))
                {
                    var xmlClass = (T)serializer.Deserialize(reader);

                    return xmlClass;
                }
            }
            catch
            {
                return null;
            }
            
        }
```
### xxController - GetAction
```csharp
public IActionResult GetCurrencyRate()
        {
            var data = ConvertXmlToClass<Tarih_Date>.ConvertXmlFrom("https://tcmb.gov.tr/kurlar/today.xml");
            if (data != null)
            {
                var result = data.Currency.Select(CurrencyModel.Map).ToArray();
                var json = JsonConvert.SerializeObject(result);

                return Ok(json);
            }
            else
            {
                return NotFound();
            }
        }
```
