using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace td2.viewModel
{
    public class ProfileViewModel:BaseViewModel
    {
        private MediaFile _mediaFile;
        public ImageItem image;
        private byte[] byteimage;
        public UserItem User { get; set; }
        public UpdateProfileRequest profile { get; set; }

        public ProfileViewModel(UserItem user)
        {
            
            User = user;
            profile = new UpdateProfileRequest();
            image = new ImageItem();
            byteimage = new byte[4];

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
                        PhotoSize = PhotoSize.Medium
                    };
                    _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                    if (_mediaFile == null) return;

                    imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                    imageView.SetValue(Image.HeightRequestProperty, 300);
                    imageView.SetValue(Image.WidthRequestProperty, 100);

                    var memoryStream = new MemoryStream();

                    _mediaFile.GetStream().CopyTo(memoryStream);
                    byteimage = memoryStream.ToArray();


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

        public async Task Update()
        {
            if (Refresh)
                return;

            Refresh = true;

            try
            {
                
                image = await restService.PostImage(byteimage);
                profile.FirstName = User.FirstName;
                profile.LastName = User.LastName;
                profile.ImageId = User.ImageId=image.Id;
                //profile.ImageUrl = User.ImageUrl;

                User=await restService.UpdateProfile(profile);

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
