using ClientApp.Model;
using ClientApp.Resources;
using Newtonsoft.Json;
using System.Text;

namespace ClientApp.View
{
    public partial class RegistrarPage : ContentPage
    {
        private readonly UsuarioModel Usuario = new() { Password = "" };

        public RegistrarPage()
        {
            InitializeComponent();
            Traducir();

            BindingContext = Usuario;
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new LoginPage());
            return true;
        }

        public void Traducir()
        {
            Titulo_Registro.Text = Global_Client.Registro;
            Id.Text = Global_Client.Identificador;
            Nombre.Text = Global_Client.Nombre;
            Contraseña.Text = Global_Client.Contrasenha;
            Guardar.Text = Global_Client.Guardar;
        }

        private async void CrearNuevo(object sender, EventArgs args)
        {
            try
            {
                if (string.IsNullOrEmpty(Usuario.Nombre))
                {
                    await DisplayAlert("Advertencia", "El nombre es obligatorio", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(Usuario.Password))
                {
                    await DisplayAlert("Advertencia", "La contraseña es obligatoria", "Ok");
                    return;
                }

                Usuario.Dinero = 0;
                Usuario.RolId = 3;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5034");
                    var json = JsonConvert.SerializeObject(Usuario);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("/api/Usuarios", content);
                    response.EnsureSuccessStatusCode();
                }

                await DisplayAlert("Información", "Usuario creado correctamente.", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Advertencia", "Ha ocurrido un error: " + ex.Message, "Ok");
            }
        }

    }
}
