using System.Globalization;
using ClientApp.Resources;
using Microsoft.VisualBasic;
using ClientApp.Sqlite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClientApp.View;

public partial class ConfigPage : ContentPage
{
    public ConfigPage()
    {
        InitializeComponent();

        Traducir();

        string code = "Español";
        switch (CultureInfo.CurrentUICulture.ToString())
        {
            case "es-ES":
                code = "Español";
                break;
            case "gl-GL":
                code = "Galego";
                break;
        }
        
        PikerLanguaje.SelectedItem = code;
    }
    protected override bool OnBackButtonPressed()
    {
        Navigation.PushAsync(new HomePage());
        return true;
    }

    public async void Traducir()
    {
        var dbContext = new DbContextSQLite(ClientApp.Sqlite.Constants.DatabasePath);
        var usuarioActual = (await dbContext.GetUsuariosAsync()).FirstOrDefault();

        Title = Global_Client.Configuracion;
        PikerLanguaje.Title = Global_Client.EscogerIdioma;
        Informacion.Text = Global_Client.Informacion;
        Nombre.Text = Global_Client.Nombre + ": " + usuarioActual.Nombre;
        Dinero.Text = Global_Client.Dinero + ": " + usuarioActual.Dinero + "€";

        ImageSource file;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5034");
            var response = await client.GetAsync($"/api/Files/like.png");
            response.EnsureSuccessStatusCode();

            byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

            file = ImageSource.FromStream(() => new MemoryStream(fileBytes));
        }

        if (file != null)
            Imagen.Source = file;
    }

    void OnChange(object sender, EventArgs e)
    {
        string seleccionado = (string) ((Picker) sender).SelectedItem;

        string language = "es-ES";
        switch (seleccionado)
        {
            case "Español": language = "es-ES";
                break;
            case "Galego": language = "gl-GL"; 
                break;
        }

        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(language);
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(language);
    }

    void CerrarSesion(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LoginPage());
    }
}