using GestorDBTFG.Model;
using Newtonsoft.Json;
using System.Windows.Input;

namespace GestorDBTFG.View;

public partial class AdministrarEtiquetasXJuego : ContentPage
{
    public List<JuegoXEtiquetaModel> ListaJuegoXEtiquetas = new();
    public JuegoModel Juego;
    public ICommand BorrarCommand { get; set; }

    public AdministrarEtiquetasXJuego(JuegoModel juego)
    {
        InitializeComponent();

        Juego = juego;
        BorrarCommand = new Command<int>(Borrar);

        _ = Init();

        Title = "Administrar etiquetas de " + Juego.Nombre;

        BindingContext = this;
    }
    protected override bool OnBackButtonPressed()
    {
        Navigation.PushAsync(new JuegosPage());
        return true;
    }


    public async Task Init()
    {
        var result = new List<JuegoXEtiquetaModel>();

        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/JuegoXEtiquetas");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<JuegoXEtiquetaModel>>(stringResult);
            }
        }
        catch (Exception ex)
        { Console.WriteLine("Error | " + ex.Message); }

        if (result != null)
            ListaJuegoXEtiquetas = result.Where(x => x.IdJuego == Juego.Id).Select(x => new JuegoXEtiquetaModel()
            {
                IdEtiqueta = x.IdEtiqueta,
                IdJuego = x.IdJuego,
                NombreJ = Juego.Nombre ?? "",
            }).ToList();

        JuegoXEtiquetasCollection.ItemsSource = ListaJuegoXEtiquetas;
    }

    private async void Borrar(int idEtiqueta)
    {
        var result = await DisplayAlert("Información", "¿Seguro que quieres eliminar este elemento?", "Sí", "No");
        if (result)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5034");
                    var response = await client.DeleteAsync($"/api/JuegoXEtiquetas/{Juego.Id}/{idEtiqueta}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                }

                await DisplayAlert("Información", "Etiqueta quitada correctamente.", "Ok");
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
        await Navigation.PushAsync(new CrearEtiquetasXJuego(Juego));
    }
}
