using ClientApp.Resources;

namespace ClientApp.View
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent(); 
            //Traducir();
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new HomePage());
            return true;
        }

        //public void Traducir()
        //{
        //    Title = Global.Casa;
        //    TablaEtiquetas.Text = Global.TablaEtiqueta;
        //    TablaJuegos.Text = Global.TablaJuego;
        //    TablaRoles.Text = Global.TablaRol;
        //    TablaUsuarios.Text = Global.TablaUsuario;
        //    Bienvenida.Text = Global.Bienvenida;
        //    Configuración.Text = Global.Configuracion;
        //}

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
