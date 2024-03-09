using IcecreamMAUI.ViewModels;
namespace IcecreamMAUI.Pages;

public partial class SigninPage : ContentPage
{
   public SigninPage(AuthViewModel authViewModel)
   {
      InitializeComponent();
      BindingContext = authViewModel;
   }

   //private async void Button_Clicked(object sender, EventArgs e)
   //{
   //   await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
   //}

   private async void SignupLabel_Tapped(object sender, TappedEventArgs e)
   {
      await Shell.Current.GoToAsync(nameof(SignupPage));
   }
}