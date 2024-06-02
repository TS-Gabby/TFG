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

        private void OnVista2Clicked(object sender, EventArgs e)
        {
            //contentView.Content = new Vista2();
        }

        private void OnVista3Clicked(object sender, EventArgs e)
        {
            //contentView.Content = new Vista3();
        }
    }
}
