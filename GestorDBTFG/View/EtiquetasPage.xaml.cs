using GestorDBTFG.Model;
using GestorDBTFG.Resources;
using Newtonsoft.Json;
using System.Windows.Input;

namespace GestorDBTFG.View;

public partial class EtiquetasPage : ContentPage
{
    public List<EtiquetaModel> ListaEtiquetas;
    public ICommand EditCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public EtiquetasPage()
    {
        InitializeComponent();

        Traducir();

        ListaEtiquetas = [new EtiquetaModel(){ Id = 0, Nombre="Default" }];
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

    private void Traducir()
    {
        Title = Global.TablaEtiqueta;
        EtiquetasCollection.EmptyView = Global.SinDatos;
        Etiquetas.Text = Global.Etiquetas;
        Nombre.Text = Global.Nombre;
        AñadirEtiqueta.SetValue(ToolTipProperties.TextProperty, Global.AñadirEtiqueta);
    }

    public async Task Init()
    {
        var result = new List<EtiquetaModel>();

        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/Etiquetas");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<EtiquetaModel>>(stringResult);
            }
        }
        catch (Exception ex)
        { Console.WriteLine("Error | " + ex.Message); }

        if (result != null)
            ListaEtiquetas = result;

        EtiquetasCollection.ItemsSource = ListaEtiquetas;
    }

    private async void Editar(int id) 
    {
        try
        {
            var result = new List<EtiquetaModel>();
            var Etiqueta = new EtiquetaModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/Etiquetas");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<EtiquetaModel>>(stringResult);
            }

            if (result != null)
            {
                Etiqueta = result.Where(x => x.Id == id).FirstOrDefault();
                await Navigation.PushAsync(new AdministrarEtiquetas(Etiqueta));
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
                    var response = await client.DeleteAsync($"/api/Etiquetas/{id}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                }

                await DisplayAlert("Información", "Etiqueta eliminada correctamente.", "Ok");
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
        await Navigation.PushAsync(new AdministrarEtiquetas(null));
    }
}
