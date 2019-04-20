using Common.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td2.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace td2.view
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PasswordPage : ContentPage
	{
        PasswordViewModel passwordViewModel;

		public PasswordPage ()
		{
			InitializeComponent ();
            BindingContext = passwordViewModel = new PasswordViewModel();

        }

        async void Save(object sender, EventArgs e)
        {
            Response res = await passwordViewModel.SaveMdp();

            if (res.IsSuccess)
            {
                await Navigation.PopAsync();
                await DisplayAlert("Mot de passe", "Nouveau mot de passe enregistré", "OK");
            }
            else
            {
                await DisplayAlert("Mot de passe", "Mot de passe non enregistré, réessayer", "OK");
            }
        }
    }
}