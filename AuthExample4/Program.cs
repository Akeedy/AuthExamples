using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "GoogleOpenID";
}).AddCookie(config =>
{
    config.LoginPath = "/Login";
    config.AccessDeniedPath = "/Denied";
    config.Events = new CookieAuthenticationEvents()
    {
        OnSigningIn = async context =>
        {

            await Task.CompletedTask;
        },
        OnSignedIn = async context =>
        {
            await Task.CompletedTask;

        },
        OnValidatePrincipal = async context =>
        {

            await Task.CompletedTask;
        },
    };
})
// .AddGoogle(options =>
// {

//     options.ClientId = "88157470680-cqop9eerln8lhavfh3gms12v34ui2f5p.apps.googleusercontent.com";
//     options.ClientSecret = "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";
//     options.CallbackPath = "/auth";
//     options.AuthorizationEndpoint += "?prompt=consent";
// })
.AddOpenIdConnect("GoogleOpenID",options=>{
    options.ClientId = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
    options.ClientSecret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
    options.CallbackPath = "/auth";
    options.Authority="https://accounts.google.com";
    options.SaveTokens=true;
    options.Events=new OpenIdConnectEvents(){
        OnTokenValidated=async context=>{
            if(context.Principal.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier).Value=="xxxxxxxxxxxxxxxxxxxxxxxxx"){
                    var claim=new Claim(ClaimTypes.Role,"Admin");
                    var claimIdentity= context.Principal.Identity as ClaimsIdentity;
                    claimIdentity.AddClaim(claim);

            }
        // "108047269108307068458"
    
        // "108047269108307068458"
        }
    };

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
