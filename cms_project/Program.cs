    using cms_project.Data;
using cms_project.Services;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHangfire(configuration =>
    configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                 .UseSimpleAssemblyNameTypeSerializer()
                 .UseRecommendedSerializerSettings()
                 .UseSqlServerStorage(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddHangfireServer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ComplaintJob>();
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth",options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/home/notfound";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanViewDashboard", policy =>
    policy.RequireClaim("Permission", "CanViewDashboard"));
    options.AddPolicy("Email Setting", policy =>
    policy.RequireClaim("Permission", "Email Setting"));
    options.AddPolicy("Manage Roles", policy =>
     policy.RequireClaim("Permission", "Manage Complaints"));
    options.AddPolicy("Manage Complaints", policy =>
  policy.RequireClaim("Permission", "Manage Roles"));
    options.AddPolicy("Resolve Complaint", policy =>
policy.RequireClaim("Permission", "Resolve Complaint"));
    options.AddPolicy("Announcement", policy =>
policy.RequireClaim("Permission", "Announcement"));

    options.AddPolicy("Manage Complaint Types", policy =>
policy.RequireClaim("Permission", "Manage Complaint Types"));
    options.AddPolicy("Manage User", policy =>
policy.RequireClaim("Permission", "Manage User"));
});



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.UseHangfireDashboard("/admin/jobs");

RecurringJob.AddOrUpdate<ComplaintJob>(
    "close-resolved-complaints-daily",
    job => job.CloseResolvedComplaints(),
    Cron.Daily()
);

app.Run();
