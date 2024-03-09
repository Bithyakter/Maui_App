using IcecreamMAUI.ViewModels;

namespace IcecreamMAUI.Pages;

public partial class SignupPage : ContentPage
{
   public SignupPage(AuthViewModel authViewModel)
   {
      InitializeComponent();
      BindingContext = authViewModel;
   }

   private async void SigninLabel_Tapped(object sender, TappedEventArgs e)
   {
      await Shell.Current.GoToAsync(nameof(SigninPage));
   }


   //private async void Button_Clicked(object sender, EventArgs e)
   //{
   //   await Shell.Current.GoToAsync($"//{nameof(SigninPage)}");
   //}
}