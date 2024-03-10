using IcecreamMAUI.Pages;
using IcecreamMAUI.Services;

namespace IcecreamMAUI
{
   public partial class AppShell : Shell
   {
      public AppShell(AuthService authService)
      {
         InitializeComponent();

         //Routing.RegisterRoute(nameof(SigninPage), typeof(SigninPage));
         //Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));

         RegisterRoutes();
         _authService = authService;
      }

      private readonly static Type[] _routablePageTypes =
        [
           typeof(SigninPage),
           typeof(SignupPage),
           typeof(MyOrdersPage),
           typeof(OrderDetailsPage),
           typeof(DetailsPage),
        ];


      private readonly AuthService _authService;

      private static void RegisterRoutes()
      {
         foreach (var pageType in _routablePageTypes)
         {
            Routing.RegisterRoute(pageType.Name, pageType);
         }
      }

      private async void FlyoutFooter_Tapped(object sender, TappedEventArgs e)
      {
         await Launcher.OpenAsync("https://www.youtube.com/@bithy");
      }

      private async void SignoutMenuItem_Clicked(object sender, EventArgs e)
      {
         //await Shell.Current.DisplayAlert("Alert", "Signout menu item clicked", "Ok");

         _authService.Signout();

         await Shell.Current.GoToAsync($"//{nameof(OnboardingPage)}");
      }
   }
}
