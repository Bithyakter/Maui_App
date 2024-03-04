using CommunityToolkit.Maui;
using IcecreamMAUI.Pages;
using IcecreamMAUI.Services;
using IcecreamMAUI.ViewModels;
using Microsoft.Extensions.Logging;
using Refit;

#if ANDROID
using System.Net.Security;
using Xamarin.Android.Net;

#elif IOS
using Security;

#endif

namespace IcecreamMAUI
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
            })

            .UseMauiCommunityToolkit();

#if DEBUG
         builder.Logging.AddDebug();
#endif

         builder.Services.AddTransient<AuthViewModel>()
                           .AddTransient<SignupPage>()
                           .AddTransient<SigninPage>();

         ConfigureRefit(builder.Services);

         return builder.Build();
      }

      private static void ConfigureRefit(IServiceCollection services)
      {
         var refitSettings = new RefitSettings
         {
            HttpMessageHandlerFactory = () =>
            {
               //return HttpMessageHandler

#if ANDROID
               return new AndroidMessageHandler
               {
                  ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
                  {
                     return certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None;
                  }
               };

#elif IOS

               return new NSUrlSessionHandler
               {
                  TrustOverrideForUrl = (NSUrlSessionHandler sender, string url, SecTrust trust) =>
                     url.StartsWith("https://localhost")
               };

#else // Default case (Windows)
               return new HttpClientHandler();
#endif
               return null;
            }
         };

         services.AddRefitClient<IAuthApi>(refitSettings)
            .ConfigureHttpClient(httpClient =>
            {
               var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                           ? "https://10.0.2.2.:7035"
                           : "https://localhost:7035";

               httpClient.BaseAddress = new Uri(baseUrl);
            });
      }
   }
}
