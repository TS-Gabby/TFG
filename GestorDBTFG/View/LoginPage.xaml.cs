using GestorDBTFG.Model;
using GestorDBTFG.Resources;
using GestorDBTFG.Sqlite;
using GestorDBTFG.Sqlite.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace GestorDBTFG.View;

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
        Configuracion.Text = Global.Configuracion;
        Titulo_IniciarSesion.Text = Global.IniciarSesion;
        Boton_IniciarSesion.Text = Global.IniciarSesion;
        Password.Text = Global.Contrasenha;
        Nombre.Text = Global.Nombre;
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


                response = await client.GetAsync("/api/Roles");
                response.EnsureSuccessStatusCode();

                var stringResult_rol = await response.Content.ReadAsStringAsync();
                result_rol = JsonConvert.DeserializeObject<List<RolModel>>(stringResult_rol);
            }

            if (result.IsNullOrEmpty() || result_rol.IsNullOrEmpty())
            {
                await DisplayAlert("Error", "Credenciales incorrectas", "OK");
                return;
            }

            var aux_rol = result_rol.Where(x => (x.Nombre??"").Equals("Admin")).FirstOrDefault(); // Solo Admins
            if (aux_rol == null)
            {
                await DisplayAlert("Error", "Credenciales incorrectas", "OK");
                return;
            }

            var aux_user = result.Where(x => x.Nombre == _loginModel.Username && x.RolId == aux_rol.Id).FirstOrDefault(); 
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

    private async void Config(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new ConfigPage());
    }
}