using ClientApp.Model;
using ClientApp.Resources;
using ClientApp.Sqlite.Models;
using Newtonsoft.Json;
using ClientApp.Sqlite;
using System.Text;
using Microsoft.EntityFrameworkCore;
using static SQLite.SQLite3;
using System.Windows.Input;

namespace ClientApp.View;

public partial class TiendaPage : ContentView
{
    private List<JuegoModel> ListaJuegos = new();

    private UsuarioActual usuarioActual;

    private UsuariosXJuegosModel? UsuarioXJuego;
    public ICommand ComprarCommand { get; set; }
    //private JuegoModel Juego = new();

    public TiendaPage()
    {
        InitializeComponent();

        _ = Init();
        Traducir();

        ComprarCommand = new Command<int>(Comprar);

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
                ListaJuegos = result.Where(j => !juegosCompradosXUsuario.Contains(j.Id)).ToList();
            }

        }
        catch (Exception ex)
        { Console.WriteLine("Error | " + ex.Message); }

        Table.ItemsSource = ListaJuegos;
    }

    public void Traducir()
    {
        Tienda.Text = Global_Client.Tienda;
    }

    private async void Comprar(int Id)
    {
        try
        {
            JuegoModel result_j;

            UsuarioXJuego = new UsuariosXJuegosModel()
            {
                IdJuego = Id,
                IdUsuario = usuarioActual.Code,
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5034");
                var response_j = await client.GetAsync($"/api/Juegos/{Id}");
                response_j.EnsureSuccessStatusCode();

                var stringResult = await response_j.Content.ReadAsStringAsync();
                result_j = JsonConvert.DeserializeObject<JuegoModel>(stringResult);

                if (result_j.Precio > usuarioActual.Dinero)
                {
                    await App.Current.MainPage.DisplayAlert("Información", "No tienes suficiente dinero", "OK");
                    return;
                }
                usuarioActual.Dinero -= result_j.Precio * (1 + result_j.Descuento/100);
                 
                var aux = new UsuarioModel()
                {
                    Id = usuarioActual.Code,
                    Password = usuarioActual.Password,
                    Dinero = usuarioActual.Dinero,
                    Nombre = usuarioActual.Nombre,
                    RolId = usuarioActual.RolId,
                };

                //Restamos el precio en el dinero del usuario
                var json = JsonConvert.SerializeObject(aux);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response_u = await client.PutAsync($"/api/Usuarios/{usuarioActual.Code}", content);
                response_u.EnsureSuccessStatusCode();

                ActualizarUsuarioActual(aux);

                //Creamos la asociación Usuario <--> Juego
                json = JsonConvert.SerializeObject(UsuarioXJuego);
                content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/UsuariosXJuegos", content);
                response.EnsureSuccessStatusCode();

                await App.Current.MainPage.DisplayAlert("Información", "Se ha comprado correctamente", "OK");

                _ = Init();
            }
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Advertencia", "Ha ocurrido un erorr: " + ex.Message, "OK");
        }
    }


    async static Task ActualizarUsuarioActual(UsuarioModel usuario)
    {
        var dbContext = new DbContextSQLite(Constants.DatabasePath);

        UsuarioActual nuevoUsuario = new()
        {
            Code = usuario.Id,
            Nombre = usuario.Nombre,
            Password = usuario.Password,
            Dinero = usuario.Dinero,
            RolId = usuario.RolId,
        };

        var usuarios = await dbContext.GetUsuariosAsync();
        if (usuarios.Count > 0)
        {
            UsuarioActual usuarioEditar = usuarios.First();
            nuevoUsuario.Id = usuarioEditar.Id;
        }
        else
        {
            nuevoUsuario.Id = 0;
        }

        await dbContext.SaveUsuarioAsync(nuevoUsuario);

    }


}
