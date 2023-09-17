var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=RedirectToAuthorization}/{id?}");
*/

app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Login}/{action=RedirectToAuthorization}/{id?}");

        endpoints.MapControllerRoute(
            name: "MyContacts",
            pattern: "{controller=MyContacts}/{action=MyContacts}/{id?}");

        endpoints.MapControllerRoute(
            name: "AddUser",
            pattern: "{controller=AddUser}/{action=AddUser}/{id?}");
    });



app.Run();
