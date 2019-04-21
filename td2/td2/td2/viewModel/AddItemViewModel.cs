using System;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

using Windows.Storage;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.Diagnostics;
using td2.view;
using Xamarin.Forms.PlatformConfiguration;
using System.IO;
using Common.Api.Dtos;

namespace td2.viewModel
{
    public class AddItemViewModel : BaseViewModel
    {
        private MediaFile _mediaFile;
        public CreatePlaceRequest Place { get; set; }
        public Map Map { get; set; }
        public ImageItem image;
        private byte[] byteimage;
        public Command LoadPin { get; set; }
        public Button PictureButton { get; set; }

        public AddItemViewModel(Map map)
        {
            LoadPin = new Command(async () => await RunPin());
            Place = new CreatePlaceRequest();
            image = new ImageItem();
            byteimage = new byte[4];

            Map = map;
            LoadPin.Execute(null);
        }

        async Task RunPin()
        {
            Map.Pins.Add(new Pin
            {
                Label = ""
            });

            var locator = await Geolocation.GetLastKnownLocationAsync();
            Map.Pins[0].Position = new Position(locator.Latitude, locator.Longitude);

            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Map.Pins[0].Position.Latitude, Map.Pins[0].Position.Longitude), Distance.FromMiles(0.5)));
        }

        public void AddPlaceItem()
        {
            Place.Latitude = Map.Pins[0].Position.Latitude;
            Place.Longitude = Map.Pins[0].Position.Longitude;
        }


        public async Task GetImageStreamAsync(Image imageView)
        {
            if (Refresh)
                return;

            Refresh = true;

            try
            {

                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    Debug.WriteLine("Error : This is not support on your device.");
                    return;
                }
                else
                {
                    var mediaOption = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Small
                    };
                    _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                    if (_mediaFile == null) return;
                   
                    imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                    imageView.SetValue(Image.HeightRequestProperty , 300);
                    imageView.SetValue(Image.WidthRequestProperty, 100);
                    
                    var memoryStream = new MemoryStream();
                   
                        _mediaFile.GetStream().CopyTo(memoryStream);
                        byteimage= memoryStream.ToArray();
                        


                }
                

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                Refresh = false;
            }
        }

        public async Task<Response> SavePlace()
        {
            Response res = null;
            if (!Refresh)
            {


                Refresh = true;

            try
            {
                image = await restService.PostImage(byteimage);
                    if (image == null)
                    {
                        Place.ImageId = 1;
                    }
                    else
                    {

                        Place.ImageId = image.Id;
                    }
                //Place.Latitude = 2;
                //Place.Longitude = 4;
                AddPlaceItem();
                res=await restService.SavePlaceItem(Place);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                Refresh = false;
                }
                return res;
            }
            return null;
        }

        public void Deplacer()
        {
            Map.Pins[0].Position = Map.VisibleRegion.Center;
        }

        public async Task PrendrePhoto(Image imageView)
        {
            if (Refresh)
                return;

            Refresh = true;

            try
            {

                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    Debug.WriteLine("No Camera", ":(No Camera available.)", "OK");
                    return;
                }
                else
                {
                    var mediaOption = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Small
                    };
                    _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "myImage.jpg"
                    });

                    if (_mediaFile == null) return;
                    imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());

                    imageView.SetValue(Image.HeightRequestProperty, 300);
                    imageView.SetValue(Image.WidthRequestProperty, 100);

                    //var memoryStream = new MemoryStream();

                    //_mediaFile.GetStream().CopyTo(memoryStream);

                    byteimage = File.ReadAllBytes(_mediaFile.Path);
                    //byteimage = memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                Refresh = false;
            }


        }


    }
}
