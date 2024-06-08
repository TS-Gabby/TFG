using GestorDBTFG.Model;
using GestorDBTFG.Resources;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace GestorDBTFG.View;

public partial class CrearEtiquetasXJuego : ContentPage
{
    private JuegoXEtiquetaModel JuegoXEtiqueta = new();

    private JuegoModel Juego;

    private List<PickerEtiquetas> EtiquetasNuevas = new();
    private PickerEtiquetas? _selectedEtiqueta;
    public PickerEtiquetas? SelectedEtiqueta
    {
        get { return _selectedEtiqueta; }
        set
        {
            if (_selectedEtiqueta != value)
            {
                _selectedEtiqueta = value;
                OnPropertyChanged(nameof(SelectedEtiqueta));
            }
        }
    }

    public CrearEtiquetasXJuego(JuegoModel juegos)
    {
        InitializeComponent();

        Traducir();

        Juego = juegos;

        JuegoXEtiqueta.IdJuego = juegos.Id;
        JuegoXEtiqueta.NombreJ = juegos.Nombre??"";

        _ = Init();

        BindingContext = JuegoXEtiqueta;
    }

    public void Traducir()
    {
        Title = Global.AñadirEtiquetaXJuego;
        Title_EtiquetaXJuego.Text = Global.EtiquetaXJuego;
        NombreJ.Text = Global.NombreJ;
        PickerEtiquetaXJuego.Title = Global.Etiqueta;
        Guardar.Text = Global.Guardar;
    }

    private async Task Init()
    {
        var result = new List<JuegoXEtiquetaModel>();
        var result_etiqueta = new List<EtiquetaModel>();

        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/Etiquetas");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result_etiqueta = JsonConvert.DeserializeObject<List<EtiquetaModel>>(stringResult);

                response = await client.GetAsync("/api/JuegoXEtiquetas");
                response.EnsureSuccessStatusCode();

                stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<JuegoXEtiquetaModel>>(stringResult);
            }
            if (!result.IsNullOrEmpty() && !result_etiqueta.IsNullOrEmpty())
            {
                result_etiqueta = result_etiqueta.Where(e => !result.Any(x => x.IdEtiqueta == e.Id && x.IdJuego == Juego.Id) ).ToList();

                EtiquetasNuevas.AddRange(result_etiqueta.Select(x => new PickerEtiquetas()
                    {
                        Id = x.Id,
                        Name = x.Nombre,
                    })
                );

                SelectedEtiqueta = new();

                if (EtiquetasNuevas.Count == 0)
                {
                    await DisplayAlert("Advertencia", "Ya existen todas las etiquetas posibles asignadas a este juego.", "Ok");
                    await Navigation.PopAsync();
                }
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Advertencia", "Ha ocurrido un error: " + ex.Message, "Ok");
        }

        PickerEtiquetaXJuego.ItemsSource = EtiquetasNuevas;
    }

    private async void Llamar(object sender, EventArgs args)
    {
        if (string.IsNullOrEmpty(SelectedEtiqueta?.Name??""))
        {
            await DisplayAlert("Advertencia", "La etiqueta no puede ser nula.", "Ok");
            return;
        }

        JuegoXEtiqueta.IdEtiqueta = SelectedEtiqueta.Id;
        JuegoXEtiqueta.NombreE = SelectedEtiqueta.Name;

        CrearNuevo();
    }

    private async void CrearNuevo()
    {
        try
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:5034");
                var json = JsonConvert.SerializeObject(new JuegoXEtiquetaDBModel() { IdEtiqueta = JuegoXEtiqueta.IdEtiqueta, IdJuego = JuegoXEtiqueta.IdJuego });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/JuegoXEtiquetas", content);
                response.EnsureSuccessStatusCode();
            }

            await DisplayAlert("Información", "Etiqueta añadida correctamente.", "Ok");
            await Navigation.PushAsync(new AdministrarEtiquetasXJuego(Juego));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Advertencia", "Ha ocurrido un error: " + ex.Message, "Ok");
        }
    }
}
