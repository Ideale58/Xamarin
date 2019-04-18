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
            Console.WriteLine(sender.ToString());
            //DisplayAlert("Alert", sender.ToString(), "OK");
            await passwordViewModel.SaveMdp();


            await Navigation.PopAsync();

        }
    }
}