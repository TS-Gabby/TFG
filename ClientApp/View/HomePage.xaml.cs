using ClientApp.Resources;

namespace ClientApp.View
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent(); 
            Traducir();

            contentView.Content = new TiendaPage();
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new HomePage());
            return true;
        }

        public void Traducir()
        {
            Title = Global_Client.Casa;
            Tienda.Text = Global_Client.Tienda; 
            Biblioteca.Text = Global_Client.Biblioteca;
            Configuracion.Text = Global_Client.Configuracion;
        }

        private async void OnTiendaClicked(object sender, EventArgs e)
        {
            contentView.Content = new TiendaPage();
        }

        private async void OnBibliotecaPage(object sender, EventArgs e)
        {
            contentView.Content = new BibliotecaPage();
        }

        private async void Config(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ConfigPage());
        }
    }
}
