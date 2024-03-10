using IcecreamMAUI.Services;

namespace IcecreamMAUI
{
   public partial class App : Application
   {
      public App(AuthService authService)
      {
         InitializeComponent();

         authService.Initialize();     //Add Later

         MainPage = new AppShell(authService);
      }
   }
}
