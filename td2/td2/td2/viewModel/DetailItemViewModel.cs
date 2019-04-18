using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace td2.viewModel
{
    public class DetailItemViewModel:BaseViewModel
    {
        public Map Map { get; set; }
        public PlaceItem Item { get; set; }

        public Command LoadMap { get; set; }

        public DetailItemViewModel(Map map, PlaceItem item = null)
        {
            LoadMap = new Command(async () => await ExecuteLoadMap());
            Map = map;
            //Title = item?.Title;
            //Description = item?.Description;
            //ImageUrl = item?.ImageUrl;
            Item = item;
            LoadMap.Execute(null);
        }
       /* public async Task NavigateToBuilding25(PlaceItem item)
        {
            Location loc = new Location(Item.Latitude, Item.Longitude);
            var placemark = new Placemark
            {
                Location = loc
            };
            var options = new MapLaunchOptions { Name = "Microsoft Building 25" };

            await Map.OpenAsync(placemark, options);
        }*/

        async Task ExecuteLoadMap()
        {
            if (Refresh)
                return;

            Refresh = true;

            var item = await restService.FindPlaceItemById(Item.Id);
            

            Item = item;


          /*  Location loc = new Location(Item.Latitude, Item.Longitude);
            var placemark = new Placemark
            {
                Location = loc
            };
            var options = new MapLaunchOptions { Name = Item.Title };

            await Map.OpenAsync(placemark, options);*/



            Map.Pins.Add(new Pin
            {
                Label = Item.Title,
                Position = new Position(Item.Latitude, Item.Longitude)
            });

            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Item.Latitude, Item.Longitude), Distance.FromMiles(0.1)));

            Refresh = false;
        }
    }
}
