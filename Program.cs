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
        var message = "Ќекорректные данные";   // содержание сообщени€ по умолчанию
        try {
            // пытаемс€ получить данные json
            var person = await request.ReadFromJsonAsync<Person>();
            if(person != null) // если данные сконвертированы в Person
                message = $"Name: {a.Search(person.Name)}";
        }
        catch { }
        // отправл€ем пользователю данные
        await response.WriteAsJsonAsync(new { text = message });
    } else if(request.Path == "/api/list") {
                
                    var message = "Ќекорректные данные";   // содержание сообщени€ по умолчанию
                    try {
                        // пытаемс€ получить данные json
                        var person = await request.ReadFromJsonAsync<Person>();
                        if(person != null) // если данные сконвертированы в Person
                            message = $"{LoadFileList.readPromStr()}";
                    }
                    catch { }
                    // отправл€ем пользователю данные
                    await response.WriteAsJsonAsync(new { text = message });
                }
            
    else {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }

    
});

app.Run();

public record Person(string Name, int Age);