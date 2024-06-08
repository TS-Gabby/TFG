using ClientApp.Model;
using ClientApp.Resources;
using ClientApp.Sqlite.Models;
using Newtonsoft.Json;
using ClientApp.Sqlite;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static SQLite.SQLite3;
using System.Windows.Input;
using ClientApp.Resources;
using Windows.Media.Protection.PlayReady;

namespace ClientApp.View;

public partial class BibliotecaPage : ContentView
{
    private List<JuegoModel> ListaJuegos = new();

    private UsuarioActual usuarioActual;

    private UsuariosXJuegosModel? UsuarioXJuego;
    public ICommand JugarCommand { get; set; }

    private string _TextoBtnJugar;
    public string TextoBtnJugar
    {
        get { return _TextoBtnJugar; }
        set
        {
            if (_TextoBtnJugar != value)
            {
                _TextoBtnJugar = value;
                OnPropertyChanged(nameof(TextoBtnJugar));
            }
        }
    }

    public BibliotecaPage()
    {
        InitializeComponent();

        _ = Init();

        JugarCommand = new Command<int>(Jugar);

        BindingContext = this;
    }

    public async Task Init()
    {
        var result = new List<JuegoModel>();
        var result_juegoUsuario = new List<UsuariosXJuegosModel>();

        try
        {
            var dbContext = new DbContextSQLite(Constants.DatabasePath);
            usuarioActual = (await dbContext.GetUsuariosAsync()).FirstOrDefault();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync("/api/Juegos");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<JuegoModel>>(stringResult);

                var response_juegoUsuario = await client.GetAsync("/api/UsuariosXJuegos");
                response_juegoUsuario.EnsureSuccessStatusCode();

                stringResult = await response_juegoUsuario.Content.ReadAsStringAsync();
                result_juegoUsuario = JsonConvert.DeserializeObject<List<UsuariosXJuegosModel>>(stringResult);
            }

            if (result != null && result_juegoUsuario != null && usuarioActual != null)
            {
                var juegosCompradosXUsuario = result_juegoUsuario.Where(x => x.IdUsuario == usuarioActual.Code).Select(x => x.IdJuego).ToList();
                ListaJuegos = result.Where(j => juegosCompradosXUsuario.Contains(j.Id)).ToList();
            }

        }
        catch (Exception ex)
        { await App.Current.MainPage.DisplayAlert("Alerta", "Error: " + ex.Message, "OK"); }

        Table.ItemsSource = ListaJuegos;
        Traducir();
    }

    public void Traducir()
    {
        Biblioteca.Text = Global_Client.Biblioteca;
        TextoBtnJugar = Global_Client.Instalar + "/" + Global_Client.Jugar;
    }

    private async void Jugar(int Id)
    {
        try
        {
            var aux = true;

            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + $"\\Downloads\\{Id}.exe"))
                aux = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response = await client.GetAsync($"/api/Files/{Id + ".exe"}");
                response.EnsureSuccessStatusCode();

                byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                await System.IO.File.WriteAllBytesAsync(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + $"\\Downloads\\{Id}.exe", fileBytes);
            }

            if (aux)
                await App.Current.MainPage.DisplayAlert("Información", "Se ha descargado correctamente", "OK");
            else
                await App.Current.MainPage.DisplayAlert("Información", "Se ejecutaría el juego", "OK");

        }
        catch (Exception ex) { }
    }

}
