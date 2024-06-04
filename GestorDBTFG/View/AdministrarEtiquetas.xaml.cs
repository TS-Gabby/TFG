using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using GestorDBTFG.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;

namespace GestorDBTFG.View;

public partial class AdministrarEtiquetas : ContentPage
{
    private readonly EtiquetaModel Etiqueta = new();

    public AdministrarEtiquetas(EtiquetaModel? etiqueta)
    {
        InitializeComponent();

        if (etiqueta != null)
        {
            Etiqueta = etiqueta;
        }

        this.Title = etiqueta == null ? "Nueva Etiqueta" : "Editar Etiqueta"; 
        BindingContext = Etiqueta;
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
