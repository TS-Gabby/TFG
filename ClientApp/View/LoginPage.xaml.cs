using ClientApp.Model;
using ClientApp.Resources;
using ClientApp.Sqlite;
using ClientApp.Sqlite.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ClientApp.View;

public partial class LoginPage : ContentPage
{
    private readonly LoginModel _loginModel = new();
    public LoginPage()
    {
        InitializeComponent();

        Traducir();

        BindingContext = _loginModel;
    }
    protected override bool OnBackButtonPressed()
    {
        Navigation.PushAsync(new LoginPage());
        return true;
    }

    public void Traducir()
    {
        Titulo_IniciarSesion.Text = Global_Client.IniciarSesion;
        Boton_IniciarSesion.Text = Global_Client.IniciarSesion;
        Password.Text = Global_Client.Contrasenha;
        Nombre.Text = Global_Client.Nombre;
        Registrar.Text = Global_Client.Registrar;
    }

    private async void Login(object sender, EventArgs e)
    {
        var result = new List<UsuarioModel>();
        var result_rol = new List<RolModel>();
        // Conexión con la BD
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/Usuarios");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<UsuarioModel>>(stringResult);
            }

            if (result.IsNullOrEmpty())
            {
                await DisplayAlert("Error", "Credenciales incorrectas", "OK");
                return;
            }

            var aux_user = result.Where(x => x.Nombre == _loginModel.Username).FirstOrDefault(); 
            if (aux_user == null)
            {
                await DisplayAlert("Error", "Credenciales incorrectas", "OK");
                return;
            }

            if (_loginModel.Password.Equals(aux_user.Password))
            {
                await CrearUsuarioActual(aux_user);
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error", "Credenciales incorrectas", "OK");
            }
        } catch (Exception ex) { await DisplayAlert("Error", "Se ha producido un error: " + ex.Message, "OK"); }


    }
    async static Task CrearUsuarioActual(UsuarioModel usuario)
    {
        var dbContext = new DbContextSQLite(Constants.DatabasePath);

        UsuarioActual nuevoUsuario = new()
        {
            Code = usuario.Id,
            Nombre = usuario.Nombre,
            Password = usuario.Password,
            Dinero = usuario.Dinero,
            RolId = usuario.RolId,
        };

        var usuarios = await dbContext.GetUsuariosAsync();
        if (usuarios.Count > 0)
        {
            UsuarioActual usuarioEditar = usuarios.First();
            nuevoUsuario.Id = usuarioEditar.Id;
        }
        else
        {
            nuevoUsuario.Id = 0;
        }

        await dbContext.SaveUsuarioAsync(nuevoUsuario);

    }

    public async static void OnRegistrarClicked(object sender, EventArgs args)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new RegistrarPage());
    }
}