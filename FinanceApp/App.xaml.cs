using Data;
using FinanceApp.ViewModels;
using FinanceApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System.Windows;

namespace FinanceApp;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider Services { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        RegisterDataTemplates();

        var services = new ServiceCollection();

        ConfigureServices(services);

        Services = services.BuildServiceProvider();

        var db = Services.GetRequiredService<AppDbContext>();
        //dotnet ef migrations add InitialCreate --project Data --startup-project FinanceApp
        db.Database.Migrate();

        var window = new MainWindow();
        //var viewModel = new MainWindowViewModel();
        var viewModel = Services.GetRequiredService<MainWindowViewModel>();
        window.DataContext = viewModel;
        window.Show();
    }

    private void RegisterDataTemplates()
    {
        var mappings = new Dictionary<Type, Type>
        {
            { typeof(MainLayoutViewModel), typeof(MainLayout) },
            { typeof(AssetUnitsListViewModel), typeof(AssetUnitsList) }
        };

        foreach (var map in mappings)
        {
            var template = new DataTemplate(map.Key)
            {
                VisualTree = new FrameworkElementFactory(map.Value)
            };
            Resources.Add(new DataTemplateKey(map.Key), template);
        }
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(
                "Data Source=app.db");
        });

        services.AddScoped<IAssetUnitRepository, AssetUnitRepository>();

        services.AddScoped<AssetUnitService>();

        services.AddScoped<MainWindowViewModel>();
        services.AddScoped<MainLayoutViewModel>();
        services.AddScoped<AssetUnitsListViewModel>();
    }
}

