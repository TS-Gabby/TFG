using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using GestorDBTFG.Model;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;

namespace GestorDBTFG.View;

public partial class EtiquetasPage : ContentPage
{
    public List<EtiquetaModel> ListaEtiquetas;
    public ICommand EditCommand { get; set; }

    public EtiquetasPage()
    {
        InitializeComponent();

        ListaEtiquetas = [new EtiquetaModel(){ Id = 0, Nombre="Default" }];
        _ = Init();

        EditCommand = new Command<int>(Editar);

        BindingContext = this;
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
            }
        }
        catch (Exception ex)
        { Console.WriteLine("Error | " + ex.Message); }
    }

    private async void CrearNuevo(object sender, EventArgs args)
    {
        Console.WriteLine("AAAA");
        await Navigation.PushAsync(new HomePage());
    }
}
