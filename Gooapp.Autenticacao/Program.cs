using Gooapp.Autenticacao.Context;
using Gooapp.Autenticacao.Profiles;
using Gooapp.Autenticacao.Services;
//using Gooapp.Autenticacao.Profiles;
//using Gooapp.Autenticacao.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.GetConnectionString("UserConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GooappDbContext>(opt => opt.UseSqlServer(configuration));
builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(opt => opt.SignIn.RequireConfirmedEmail = true)
    .AddEntityFrameworkStores<GooappDbContext>()
    .AddDefaultTokenProviders();



//builder.Services.AddDefaultIdentity<ApplicationUser>()
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<GooappDbContext>();


builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();


//builder.Services.AddScoped<IUrlHelper>(x => {
//    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
//    var factory = x.GetRequiredService<IUrlHelperFactory>();
//    return factory.GetUrlHelper(actionContext);
//});


//builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
//                .AddScoped(x =>
//                    x.GetRequiredService<IUrlHelperFactory>()
//                        .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));

builder.Services.AddScoped<CadastroService, CadastroService>();
builder.Services.AddScoped<EmailService, EmailService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<LoginService, LoginService>();
builder.Services.AddAutoMapper(typeof(UsuarioProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
