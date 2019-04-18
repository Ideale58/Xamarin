using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace td2.viewModel
{
    class RegisterViewModel:BaseViewModel
    {
        public RegisterRequest User { get; set; }
        public RegisterViewModel()
        {
            User = new RegisterRequest();
        }

        public async Task SaveUser()
        {
            if (Refresh)
                return;

            Refresh = true;

            try
            {
                await restService.SaveUser(User);

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
