using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace td2.viewModel
{
    class LoginViewModel:BaseViewModel
    {
        public LoginRequest User { get; set; }
        public LoginViewModel()
        {
            User = new LoginRequest();
        }

        public async Task ConnexionUser()
        {
            if (Refresh)
                return;

            Refresh = true;

            try
            {
                await restService.ConnexionUser(User);

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
