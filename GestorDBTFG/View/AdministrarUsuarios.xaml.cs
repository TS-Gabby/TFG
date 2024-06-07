using GestorDBTFG.Model;
using GestorDBTFG.Resources;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace GestorDBTFG.View;

public partial class AdministrarUsuarios : ContentPage
{
    public List<PickerRoles> Roles = new();
    private UsuarioModel Usuario = new() { Password = "" };

    public PickerRoles? SelectedRol { get; set; }

    public AdministrarUsuarios(UsuarioModel? usuario)
    {
        InitializeComponent();

        Traducir();

        if (usuario != null)
        {
            Usuario = usuario;
        }

        this.Title = usuario == null ? "Nuevo Usuario" : "Editar Usuario";
        _ = Init();

        BindingContext = Usuario;
    }

    public void Traducir()
    {
        Title = Global.AdministrarUsuario;
        Titulo_Usuario.Text = Global.Usuario;
        Id.Text = Global.Identificador;
        Contraseña.Text = Global.Contrasenha;
        Dinero.Text = Global.Dinero;
        PickerRoles.Title = Global.Roles;
        Guardar.Text = Global.Guardar;
    }

    private async Task Init()
    {
        var result = new List<RolModel>();

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5034");
            var response = await client.GetAsync("/api/Roles");
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<List<RolModel>>(stringResult);
        }
        if (!result.IsNullOrEmpty())
        {
            Roles.AddRange(
                result.Select(x => new PickerRoles() 
                { 
                    Id = x.Id, Name = x.Nombre ?? string.Empty 
                })
            );

            SelectedRol = result.Where(x => x.Id == Usuario.RolId).Select(x => new PickerRoles()
            {
                Id = x.Id,
                Name = x.Nombre ?? string.Empty
            }).FirstOrDefault();
        }

        PickerRoles.ItemsSource = Roles;
    }

    private async void Llamar(object sender, EventArgs args)
    {
        if (string.IsNullOrEmpty(Usuario.Nombre))
        {
            await DisplayAlert("Advertencia", "El nombre no puede ser nulo.", "Ok");
            return;
        }
        if (string.IsNullOrEmpty(Usuario.Password))
        {
            await DisplayAlert("Advertencia", "La contraseña no puede ser nula.", "Ok");
            return;
        }
        if (SelectedRol == null)
        {
            await DisplayAlert("Advertencia", "Debe elegir un rol.", "Ok");
            return;
        }

        Usuario.RolId = SelectedRol.Id;

        if (Usuario.Id <= 0)
        {
            CrearNuevo();
        }
        else
        {
            Editar();
        }
    }

    private async void CrearNuevo()
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var json = JsonConvert.SerializeObject(Usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/Usuarios", content);
                response.EnsureSuccessStatusCode();
            }

            await DisplayAlert("Información", "Usuario creado correctamente.", "Ok");
            await Navigation.PushAsync(new UsuariosPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Advertencia", "Ha ocurrido un error: " + ex.Message, "Ok");
        }
    }
    private async void Editar()
    {

        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var json = JsonConvert.SerializeObject(Usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"/api/Roles/{Usuario.Id}", content);
                response.EnsureSuccessStatusCode();
            }

            await DisplayAlert("Información", "Usuario editado correctamente.", "Ok");
            await Navigation.PushAsync(new RolesPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Advertencia", "Ha ocurrido un error: " + ex.Message, "Ok");
        }
    }

}
