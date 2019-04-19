using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using td2.data;
using td2.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace td2.view
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlaceItemPage : ContentPage
	{
        PlaceItemViewModel placeItemViewModel;
        public Command LoadCommand2 { get; set; }

        public PlaceItemPage ()
		{
			InitializeComponent ();
            BindingContext = placeItemViewModel = new PlaceItemViewModel();
       
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (RestService.TOKEN == null)
            {
                Addplace.IsEnabled = false;
            }
            else
            {
                Addplace.IsEnabled = true;
            }
            if (placeItemViewModel.Items.Count == 0)
                placeItemViewModel.LoadCommand.Execute(null);
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //DisplayAlert("Alert", "You have been alerted", "OK");
             var item = args.SelectedItem as PlaceItemSummary;
             if (item == null)
                 return;

            //DisplayAlert("Item Tapped", ((PlaceItemSummary)args.SelectedItem).Title.ToString(), "Ok");
             PlaceItem place = await placeItemViewModel.restService.FindPlaceItemById(item.Id);
             await Navigation.PushAsync(new ItemDetailPage(place));
            
             // Manually deselect item.
             ItemsListView.SelectedItem = null;
        }
        async void AjouterLieu(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new AddItemPage());
            //await Navigation.PushModalAsync(new AddItemPage());
            
        }

       
    }
}