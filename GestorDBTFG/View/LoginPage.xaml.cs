using GestorDBTFG.Model;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System;

namespace GestorDBTFG.View;

public partial class LoginPage : ContentPage
{
    private readonly LoginModel _loginModel = new();
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = _loginModel;
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

            var aux_user = result.Where(x => x.Nombre == _loginModel.Username && x.RolId == aux_rol.Id).First(); 
            if (aux_user == null)
            {
                await DisplayAlert("Error", "Credenciales incorrectas", "OK");
                return;
            }

            if (_loginModel.Password.Equals(aux_user.Password))
            {
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error", "Credenciales incorrectas", "OK");
            }
        } catch { await DisplayAlert("Error", "Credenciales incorrectas", "OK"); }

    }
}