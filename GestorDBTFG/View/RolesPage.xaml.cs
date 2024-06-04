using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using GestorDBTFG.Model;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;

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
            var Etiqueta = new RolModel();

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
                Etiqueta = result.Where(x => x.Id == id).FirstOrDefault();
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

        }
    }

    private async void CrearNuevo(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new HomePage());
    }
}
