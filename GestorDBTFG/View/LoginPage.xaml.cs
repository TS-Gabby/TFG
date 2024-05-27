using GestorDBTFG.Model;
using Newtonsoft.Json;

namespace GestorDBTFG.View;

public partial class LoginPage : ContentPage
{
    private readonly LoginModel _loginModel = new();
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = _loginModel;
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        var result = new List<UsuarioModel>();
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
        }
        catch { }

        if (result == null)
            return;

        var aux = result.Where(x => x.Nombre == _loginModel.Username).First();
        if (aux == null)
            return;

        if (_loginModel.Password.Equals(aux.Password))
        {
            var a = "1";
        }
        else
        {
            var a = "2";
        }

    }
}