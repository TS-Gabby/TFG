using GestorDBTFG.Model;
using GestorDBTFG.Resources;
using Newtonsoft.Json;
using System.Text;

namespace GestorDBTFG.View;

public partial class AdministrarEtiquetas : ContentPage
{
    private readonly EtiquetaModel Etiqueta = new();

    public AdministrarEtiquetas(EtiquetaModel? etiqueta)
    {
        InitializeComponent();

        Traducir();

        if (etiqueta != null)
        {
            Etiqueta = etiqueta;
        }

        this.Title = etiqueta == null ? "Nueva Etiqueta" : "Editar Etiqueta"; 
        BindingContext = Etiqueta;
    }

    public void Traducir()
    {
        Title = Global.AdministrarEtiquetas;
        Title_Etiqueta.Text = Global.Etiqueta;
        Id.Text = Global.Identificador;
        Nombre.Text = Global.Nombre;
        Guardar.Text = Global.Guardar;
    }

    private async void Llamar(object sender, EventArgs args)
    {
        if (string.IsNullOrEmpty(Etiqueta.Nombre))
        {
            await DisplayAlert("Advertencia", "El nombre no puede ser nulo.", "Ok");
            return;
        }

        if (Etiqueta.Id <= 0)
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
                var json = JsonConvert.SerializeObject(Etiqueta);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/Etiquetas", content);
                response.EnsureSuccessStatusCode();
            }

            await DisplayAlert("Información", "Etiqueta creada correctamente.", "Ok");
            await Navigation.PushAsync(new EtiquetasPage());
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
                var json = JsonConvert.SerializeObject(Etiqueta);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"/api/Etiquetas/{Etiqueta.Id}", content);
                response.EnsureSuccessStatusCode();
            }

            await DisplayAlert("Información", "Etiqueta editada correctamente.", "Ok");
            await Navigation.PushAsync(new EtiquetasPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Advertencia", "Ha ocurrido un error: " + ex.Message, "Ok");
        }
    }

}
