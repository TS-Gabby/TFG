using GestorDBTFG.Model;
using GestorDBTFG.Resources;
using Newtonsoft.Json;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestorDBTFG.View;

public partial class JuegosPage : ContentPage
{
    public List<JuegoModel> ListaJuegos;
    public string AñadirEtiqueta;
    public ICommand EditCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public ICommand EditTagCommand { get; set; }

    public JuegosPage()
    {
        InitializeComponent();

        Traducir();

        ListaJuegos = [new JuegoModel(){ Id = 0, Nombre="Default" }];
        _ = Init();

        EditCommand = new Command<int>(Editar);
        DeleteCommand = new Command<int>(Borrar);
        EditTagCommand = new Command<int>(EditarTag);

        BindingContext = this;
    }
    protected override bool OnBackButtonPressed()
    {
        Navigation.PushAsync(new HomePage());
        return true;
    }

    public void Traducir()
    {
        Title = Global.TablaJuego;
        JuegosCollection.EmptyView = Global.SinDatos;
        Id.Text = Global.Identificador;
        Nombre.Text = Global.Nombre;
        Compañia.Text = Global.Compañia;
        PC.Text = Global.DisponiblePC;
        Movil.Text = Global.DisponibleM;
        Porcentaje.Text = Global.Porcentaje;
        Precio.Text = Global.Precio;
        Añadir.SetValue(ToolTipProperties.TextProperty, Global.AñadirJuego);
        AñadirEtiqueta = Global.AñadirEtiquetaXJuego;
    }

    public async Task Init()
    {
        var result = new List<JuegoModel>();

        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/Juegos");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<JuegoModel>>(stringResult);
            }
        }
        catch (Exception ex)
        { Console.WriteLine("Error | " + ex.Message); }

        if (result != null)
            ListaJuegos = result;

        JuegosCollection.ItemsSource = ListaJuegos;
    }

    private async void Editar(int id)
    {
        var Juego = new JuegoModel();

        try
        {
            var result = new List<JuegoModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/Juegos");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<JuegoModel>>(stringResult);
            }

            if (result != null)
            {
                Juego = result.Where(x => x.Id == id).FirstOrDefault();
            }
        }
        catch (Exception ex)
        { Console.WriteLine("Error | " + ex.Message); }

        await Navigation.PushAsync(new AdministrarJuegos(Juego));
    }
    private async void EditarTag(int id)
    {
        var Juego = new JuegoModel();

        try
        {
            var result = new List<JuegoModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/Juegos");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<JuegoModel>>(stringResult);
            }

            if (result != null)
            {
                Juego = result.Where(x => x.Id == id).FirstOrDefault();
            }
        }
        catch (Exception ex)
        { Console.WriteLine("Error | " + ex.Message); }

        await Navigation.PushAsync(new AdministrarEtiquetasXJuego(Juego));
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
                    var response = await client.DeleteAsync($"/api/Juegos/{id}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                }

                await DisplayAlert("Información", "Juego eliminado correctamente.", "Ok");
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
        await Navigation.PushAsync(new AdministrarJuegos(null));
    }
}
