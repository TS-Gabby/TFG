using GestorDBTFG.Resources;

namespace GestorDBTFG.View
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent(); 
            Traducir();
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new HomePage());
            return true;
        }

        public void Traducir()
        {
            Title = Global.Casa;
            TablaEtiquetas.Text = Global.TablaEtiqueta;
            TablaJuegos.Text = Global.TablaJuego;
            TablaRoles.Text = Global.TablaRol;
            TablaUsuarios.Text = Global.TablaUsuario;
            Bienvenida.Text = Global.Bienvenida;
            Configuración.Text = Global.Configuracion;
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
