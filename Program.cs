using ConsoleApp1;

ListPromotion a = new ListPromotion();
List<string> ta = LoadFileList.readPromList();
foreach(string tb in ta) {
    a.Add(new PromotionComp(tb));
}

var builder = WebApplication.CreateBuilder();
var app = builder.Build();
app.Run(async (context) => {
    var response = context.Response;
    var request = context.Request;
    //context.Response.ContentType = "text/plain;charset=utf-8";
    //await context.Response.WriteAsync(LoadFileList.readPromStr());
    if(request.Path == "/api/user") {
        var message = "Íåêîððåêòíûå äàííûå";   // ñîäåðæàíèå ñîîáùåíèÿ ïî óìîë÷àíèþ
        try {            
            var person = await request.ReadFromJsonAsync<Person>();
            if(person != null) // åñëè äàííûå ñêîíâåðòèðîâàíû â Person
                message = $"Name: {a.Search(person.Name)}";
        }
        catch { }        
        await response.WriteAsJsonAsync(new { text = message });
    } else if(request.Path == "/api/list") {
                
                    var message = "Íåêîððåêòíûå äàííûå";   // ñîäåðæàíèå ñîîáùåíèÿ ïî óìîë÷àíèþ
                    try {                        
                        var person = await request.ReadFromJsonAsync<Person>();
                        if(person != null) // åñëè äàííûå ñêîíâåðòèðîâàíû â Person
                            message = $"{LoadFileList.readPromStr()}";
                    }
                    catch { }                    
                    await response.WriteAsJsonAsync(new { text = message });
                }
            
    else {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }

    
});

app.Run();

public record Person(string Name, int Age);
