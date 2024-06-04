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
using GestorDBTFG.Components.Pages;

namespace GestorDBTFG.View;

public partial class AdministrarRoles : ContentPage
{
    private readonly RolModel Rol = new();

    public AdministrarRoles(RolModel? rol)
    {
        InitializeComponent();

        if (rol != null)
        {
            Rol = rol;
        }

        this.Title = rol == null ? "Nuevo Rol" : "Editar Rol"; 
        BindingContext = Rol;
    }

    private async void Llamar(object sender, EventArgs args)
    {
        if (string.IsNullOrEmpty(Rol.Nombre))
        {
            await DisplayAlert("Advertencia", "El nombre no puede ser nulo.", "Ok");
            return;
        }

        if (Rol.Id <= 0)
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
                var json = JsonConvert.SerializeObject(Rol);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/Roles", content);
                response.EnsureSuccessStatusCode();
            }

            await DisplayAlert("Información", "Rol creado correctamente.", "Ok");
            await Navigation.PushAsync(new RolesPage());
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
                var json = JsonConvert.SerializeObject(Rol);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"/api/Roles/{Rol.Id}", content);
                response.EnsureSuccessStatusCode();
            }

            await DisplayAlert("Información", "Rol editado correctamente.", "Ok");
            await Navigation.PushAsync(new RolesPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Advertencia", "Ha ocurrido un error: " + ex.Message, "Ok");
        }
    }

}
