using Microsoft.Maui;
using GestorDBTFG.Model;
using Microsoft.Maui.Controls;
using System;


namespace GestorDBTFG.View
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
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
    }
}
