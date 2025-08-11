using Library_App.Models.ViewModel;
using Library_App.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using Library_App.Pages;

namespace Library_App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<APIService>();

            builder.Services.AddSingleton<IBookService, BookService>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<BookViewModel>();
            builder.Services.AddTransient<BookCreatePage>();
            builder.Services.AddTransient<BookDetailPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
