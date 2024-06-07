using GestorDBTFG.Model;
using GestorDBTFG.Resources;
using Newtonsoft.Json;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestorDBTFG.View;

public partial class UsuariosPage : ContentPage
{
    public List<UsuarioModel> ListaUsuarios;
    public ICommand EditCommand { get; set; }
    public ICommand BorrarCommand { get; set; }

    public UsuariosPage()
    {
        InitializeComponent();

        Traducir();

        ListaUsuarios = [new UsuarioModel(){ Id = 0, Nombre="Default", Password="1234" }];
        _ = Init();

        EditCommand = new Command<int>(Editar);
        BorrarCommand = new Command<int>(Borrar);

        BindingContext = this;
    }
    protected override bool OnBackButtonPressed()
    {
        Navigation.PushAsync(new HomePage());
        return true;
    }

    public void Traducir()
    {
        Title = Global.TablaUsuario;
        UsuariosCollection.EmptyView = Global.SinDatos;
        Añadir.SetValue(ToolTipProperties.TextProperty, Global.AñadirUsuario);
        Usuarios.Text = Global.Usuario;
        Id.Text = Global.Identificador;
        Nombre.Text = Global.Nombre;
        Contraseña.Text = Global.Contrasenha;
        Dinero.Text = Global.Nombre;
    }

    public async Task Init()
    {
        var result = new List<UsuarioModel>();

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
        catch (Exception ex)
        { Console.WriteLine("Error | " + ex.Message); }

        if (result != null)
            ListaUsuarios = result;

        UsuariosCollection.ItemsSource = ListaUsuarios;
    }

    private async void Editar(int id) 
    {
        try
        {
            var result = new List<UsuarioModel>();
            var Usuario = new UsuarioModel() { Password="1234" };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/Usuarios");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<UsuarioModel>>(stringResult);
            }

            if (result != null)
            {
                Usuario = result.Where(x => x.Id == id).FirstOrDefault();
            }

            await Navigation.PushAsync(new AdministrarUsuarios(Usuario));
        }
        catch (Exception ex)
        { Console.WriteLine("Error | " + ex.Message); }
    }
    private async void Borrar(int id)
    {
        var result = await DisplayAlert("Información", "¿Seguro que quieres eliminar este elemento?", "Sí", "No");
        if (result)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5034");
                    var response = await client.DeleteAsync($"/api/Usuarios/{id}");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                }

                await DisplayAlert("Información", "Usuario eliminado correctamente.", "Ok");
                await Init();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Advertencia", "Ha ocurrido un error: " + ex.Message, "Ok");
            }
        }
    }
    
    private async void CrearNuevo(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new AdministrarUsuarios(null));
    }

}
