﻿using System;
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
	public partial class LoginPage : ContentPage
    {
        LoginViewModel loginViewModel;
        public LoginPage ()
		{
			InitializeComponent ();
            BindingContext = loginViewModel = new LoginViewModel();
        }

        async void Connexion(object sender, EventArgs e)
        {
            LoginResult res = await loginViewModel.ConnexionUser();
            if (res != null)
            {
                Application.Current.MainPage=new MainPage();
                await DisplayAlert("Connexion", "Utilisateur connecté", "OK");
            }
            else
            {
                await DisplayAlert("Connexion", "Mauvais identifiants", "OK");
            }
        }
    }
}