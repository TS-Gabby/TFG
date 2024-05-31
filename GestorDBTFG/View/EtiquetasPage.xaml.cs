using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using GestorDBTFG.Model;
using Newtonsoft.Json;

namespace GestorDBTFG.View;

public partial class EtiquetasPage : ContentPage
{
    private static readonly List<EtiquetaModel> Etiquetas = new();
    public EtiquetasPage()
	{
		InitializeComponent();
    }

    public async Task GetEtiquetas()
    {
        var result = new List<UsuarioModel>();
        // Conexión con la BD
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

        //Etiquetas = result;
    }

    //public void Post(MiModelo item)
    //{
    //    Items.Add(item);
    //}
}
