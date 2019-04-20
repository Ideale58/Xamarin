using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td2.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Media;
using Plugin.Media.Abstractions;
using TD.Api.Dtos;
using td2.data;
using Common.Api.Dtos;

namespace td2.view
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddItemPage : ContentPage
    {
        
        public AddItemViewModel AddItemViewModel { get; set; }
        public AddItemPage ()
		{
			InitializeComponent ();
            BindingContext = AddItemViewModel = new AddItemViewModel(map);
        }
        

        async void Upload(object sender, EventArgs e)
        {
            await AddItemViewModel.GetImageStreamAsync(imageView);
        }
        async void Save_Clicked(object sender, EventArgs e)
        {

            Response res = await AddItemViewModel.SavePlace();
            if (res.IsSuccess)
            {
                Application.Current.MainPage = new MainPage();
                //await Navigation.PopAsync();
                await DisplayAlert("Lieu", "Lieu ajouté", "OK");
            }
            else
            {
                await DisplayAlert("Lieu", "Lieu non ajouté, réessayer", "OK");
            }
        }
        public void Deplacer(object sender, EventArgs e)
         {
             AddItemViewModel.Deplacer();
         }

        async void Photographier(object sender, EventArgs e)
        {
            await AddItemViewModel.PrendrePhoto(imageView);
        }

        /*async void Save_Clicked(object sender, EventArgs e)
        {

        }

        async void Button_Clicked(object sender, EventArgs e)
        {

        }*/
    }
}