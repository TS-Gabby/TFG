using Microsoft.Maui;
using GestorDBTFG.Model;
using Microsoft.Maui.Controls;
using GestorDBTFG.Sqlite;
using System;


namespace GestorDBTFG.View
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new LoginPage());
            return true;
        }

        private async void OnEtiquetasPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EtiquetasPage());
        }

        private async void OnRolesPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RolesPage());
        }

        private async void OnJuegosPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new JuegosPage());
        }

        private async void OnUsuariosPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsuariosPage());
        }

        private async void Config(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ConfigPage());
        }
    }
}
