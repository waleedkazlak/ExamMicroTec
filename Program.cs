using Autofac;
using Autofac.Extensions.DependencyInjection;
using ExamMicroTec.DAL;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddAutofac();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterAssemblyTypes((Assembly.Load(nameof(ExamMicroTec))))
    .Where(x => x.Namespace != null && x.Namespace.StartsWith("ExamMicroTec.Services"))
    .As(t => t.GetInterfaces()).AsImplementedInterfaces();
    });

var app = builder.Build();



    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");



    app.Run();
