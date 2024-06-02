using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using GestorDBTFG.Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace GestorDBTFG.View;

public partial class EtiquetasPage : ContentPage
{
    private readonly static ObservableCollection<EtiquetaModel> Etiquetas = new();
    public EtiquetasPage()
	{
		InitializeComponent();
        _ = Init();
    }

    [Obsolete]
    public async Task Init()
    {
        var result = new List<EtiquetaModel>();
        // Conexión con la BD
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
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Etiquetas.Clear();
                for (int i = 0; i < result.Count; i++)
                {
                    EtiquetaModel? item = result[i];
                    Etiquetas.Add(item);
                }

                BindingContext = Etiquetas;
            });
        }
    }

    //public void Post(MiModelo item)
    //{
    //    Items.Add(item);
    //}
}
