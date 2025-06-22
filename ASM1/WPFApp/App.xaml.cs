using Microsoft.Extensions.Configuration;
using System.Windows;

namespace WPFApp
{
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; }

        static App()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }
    }
}