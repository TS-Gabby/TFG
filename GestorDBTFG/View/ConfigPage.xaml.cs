using System.Globalization;
using GestorDBTFG.Resources;
using GestorDBTFG.Sqlite;

namespace GestorDBTFG.View;

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
        var dbContext = new DbContextSQLite(Constants.DatabasePath);
        var usuarioActual = (await dbContext.GetUsuariosAsync()).FirstOrDefault();

        Title = Global.Configuracion;
        PikerLanguaje.Title = Global.EscogerIdioma;
        Informacion.Text = Global.Informacion;
        Nombre.Text = Global.Nombre + ": " + usuarioActual.Nombre;
        Dinero.Text = Global.Dinero + ": " + usuarioActual.Dinero + "€";
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