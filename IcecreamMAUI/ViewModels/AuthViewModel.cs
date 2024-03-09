using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IcecreamMAUI.Pages;
using IcecreamMAUI.Services;
using IcecreamMAUI.Shared.Dtos;

namespace IcecreamMAUI.ViewModels
{
   public partial class AuthViewModel(IAuthApi authApi, AuthService authService) : BaseViewModel
   {
      private readonly IAuthApi _authApi = authApi;
      private readonly AuthService _authService = authService;

      [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
      private string? _name;

      [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
      private string? _email;

      [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
      private string? _password;

      [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
      private string? _address;

      public bool CanSignup => !string.IsNullOrEmpty(Name)
                           && !string.IsNullOrEmpty(Email)
                           && !string.IsNullOrEmpty(Password)
                           && !string.IsNullOrEmpty(Address);

      public bool CanSignin => !string.IsNullOrEmpty(Email)
                          && !string.IsNullOrEmpty(Password);

      [RelayCommand]
      private async Task SignupAsync()
      {
         IsBusy = true;
         try
         {
            var signupDto = new SignupRequestDto(Name, Email, Password, Address);

            var result = await _authApi.SignupAsync(signupDto);

            if (result.IsSuccess)
            {
               await GoToAsync($"//{nameof(HomePage)}", animate: true);
            }
            else
            {
               await ShowErrorAlertAsync(result.ErrorMessage ?? "Unknown error in signing up");
            }
         }
         catch (Exception ex)
         {
            await ShowErrorAlertAsync(ex.Message);
         }
         finally
         {
            IsBusy = false;
         }
      }

      [RelayCommand]
      private async Task SigninAsync()
      {
         IsBusy = true;
         try
         {
            var signinto = new SigninRequestDto(Email, Password);

            var result = await _authApi.SigninAsync(signinto);

            if (result.IsSuccess)
            {
               await ShowAlertAsync(result.Data.Token);

               await GoToAsync($"//{nameof(HomePage)}", animate: true);
            }
            else
            {
               await ShowErrorAlertAsync(result.ErrorMessage ?? "Unknown error in signing up");
            }
         }
         catch (Exception ex)
         {
            await ShowErrorAlertAsync(ex.Message);
         }
         finally
         {
            IsBusy = false;
         }
      }
   }
}