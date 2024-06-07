using GestorDBTFG.Model;
using Newtonsoft.Json;
using System.Windows.Input;

namespace GestorDBTFG.View;

public partial class RolesPage : ContentPage
{
    public List<RolModel> ListaRoles;
    public ICommand EditCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public RolesPage()
    {
        InitializeComponent();

        ListaRoles = [new RolModel(){ Id = 0, Nombre="Default" }];
        _ = Init();

        EditCommand = new Command<int>(Editar);
        DeleteCommand = new Command<int>(Borrar);

        BindingContext = this;
    }
    protected override bool OnBackButtonPressed()
    {
        Navigation.PushAsync(new HomePage());
        return true;
    }

    public async Task Init()
    {
        var result = new List<RolModel>();

        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/Roles");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<RolModel>>(stringResult);
            }
        }
        catch (Exception ex)
        { Console.WriteLine("Error | " + ex.Message); }

        if (result != null)
            ListaRoles = result;

        RolesCollection.ItemsSource = ListaRoles;
    }

    private async void Editar(int id) 
    {
        try
        {
            var result = new List<RolModel>();
            var Rol = new RolModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/Roles");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<RolModel>>(stringResult);
            }

            if (result != null)
            {
                Rol = result.Where(x => x.Id == id).FirstOrDefault();
                await Navigation.PushAsync(new AdministrarRoles(Rol));
            }
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
                    var response = await client.GetAsync("/api/Usuarios");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var aux = JsonConvert.DeserializeObject<List<UsuarioModel>>(stringResult);

                    if (aux?.Any(x => x.RolId == id) ?? false)
                    {
                        await DisplayAlert("Advertencia", "El Rol que intenta eliminar está en uso.", "Ok");
                        return;
                    }

                    response = await client.DeleteAsync($"/api/Roles/{id}");
                    response.EnsureSuccessStatusCode();

                    stringResult = await response.Content.ReadAsStringAsync();
                }

                await DisplayAlert("Información", "Rol eliminado correctamente.", "Ok");
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
        await Navigation.PushAsync(new AdministrarRoles(null));
    }
}
