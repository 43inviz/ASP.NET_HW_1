using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();

//app.MapGet("/", () => "Hello World!");

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    string path = request.Path.ToString().ToLower();
    if(path == "/")
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("Html/index.html");
    }
    else if(path == "/from" && request.Method == "POST")
    {
        string userName = request.Form["userName"];
        string userPhone = request.Form["UserPhone"];
        await response.WriteAsync($"User name - {userName}{Environment.NewLine}User phone - {userPhone}");
    }
    else if (path == "/from")
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("Html/form.html");
    }
    else
    {
        response.StatusCode = 404;
        await response.WriteAsync("NotFound");
    }
});

app.Run();
