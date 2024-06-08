using System.Globalization;
using ClientApp.Resources;

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

    public void Traducir()
    {
        Title = Global.Configuracion;
        PikerLanguaje.Title = Global.EscogerIdioma;
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