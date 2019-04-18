using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using td2.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace td2.view
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        RegisterViewModel registerViewModel;
		public RegisterPage ()
		{
			InitializeComponent ();
            BindingContext = registerViewModel = new RegisterViewModel();
        }

        async void Save(object sender, EventArgs e) {
            Console.WriteLine(sender.ToString());
            //DisplayAlert("Alert", sender.ToString(), "OK");
            await registerViewModel.SaveUser();


            await Navigation.PushModalAsync(new MainPage());

        }

    }
}