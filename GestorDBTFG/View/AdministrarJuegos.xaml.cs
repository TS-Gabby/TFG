using GestorDBTFG.Model;
using GestorDBTFG.Resources;
using Newtonsoft.Json;
using System.Text;

namespace GestorDBTFG.View;

public partial class AdministrarJuegos : ContentPage
{
    private JuegoModel Juego = new();

    public AdministrarJuegos(JuegoModel? juegos)
    {
        InitializeComponent();

        Traducir();

        if (juegos != null)
        {
            Juego = juegos;
        }

        this.Title = juegos == null ? "Nuevo Juego" : "Editar Juego";

        BindingContext = Juego;
    }

    public void Traducir()
    {
        Title = Global.AdministrarJuegos;
        Title_Juego.Text = Global.Juego;
        Id.Text = Global.Identificador;
        Nombre.Text = Global.Nombre;
        Compañía.Text = Global.Compañia;
        Juego_PC.Text = Global.JuegoPC;
        Juego_M.Text = Global.JuegoM;
        Descuento.Text = Global.Descuento;
        Precio.Text = Global.Precio;
        Guardar.Text = Global.Guardar;
    }

    private async void Llamar(object sender, EventArgs args)
    {
        if (string.IsNullOrEmpty(Juego.Nombre))
        {
            await DisplayAlert("Advertencia", "El nombre no puede ser nulo.", "Ok");
            return;
        }
        if (string.IsNullOrEmpty(Juego.Compania))
        {
            await DisplayAlert("Advertencia", "La compañía no puede ser nula.", "Ok");
            return;
        }

        if (Juego.Id <= 0)
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
                var json = JsonConvert.SerializeObject(Juego);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/Juegos", content);
                response.EnsureSuccessStatusCode();
            }

            await DisplayAlert("Información", "Juego creado correctamente.", "Ok");
            await Navigation.PushAsync(new JuegosPage());
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
                var json = JsonConvert.SerializeObject(Juego);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"/api/Juegos/{Juego.Id}", content);
                response.EnsureSuccessStatusCode();
            }

            await DisplayAlert("Información", "Juego editado correctamente.", "Ok");
            await Navigation.PushAsync(new JuegosPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Advertencia", "Ha ocurrido un error: " + ex.Message, "Ok");
        }
    }

}
