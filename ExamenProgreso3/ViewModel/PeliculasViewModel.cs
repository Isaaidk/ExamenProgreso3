using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ExamenProgreso3.Models;
using System.Windows.Input;

namespace ExamenProgreso3.ViewModel
{
    public class PeliculasViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;


        public ObservableCollection<Peliculas> Peliculas { get; set; } = new ObservableCollection<Peliculas>();
        private string _searchText;
        private Peliculas _selectedPelicula;
        private string _errorMessage;
        private bool _isLoading;

        public PeliculasViewModel()
        {
            _httpClient = new HttpClient();
            BuscarPeliculaCommand = new Command(async () => await BuscarPeliculaAsync());


        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
            }
        }

        public Peliculas SelectedPelicula
        {
            get => _selectedPelicula;
            set
            {
                SetProperty(ref _selectedPelicula, value);
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                SetProperty(ref _errorMessage, value);
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                SetProperty(ref _isLoading, value);
            }
        }

        public ICommand BuscarPeliculaCommand { get; }

        public string FormattedGenres
        {
            get
            {
                // Validar que el array no sea nulo y tenga al menos un elemento
                return SelectedPelicula?.genre != null && SelectedPelicula.genre.Length > 0
                    ? SelectedPelicula.genre[0] // Acceder directamente al primer elemento
                    : "No géneros disponibles";
            }
        }


        public string FormattedActors
        {
            get
            {
                if (SelectedPelicula?.actors != null && SelectedPelicula.actors.Length > 0)
                {
                    // Devolver solo el primer actor
                    return SelectedPelicula.actors.First();
                }
                return "No actores disponibles";
            }
        }




        private async Task BuscarPeliculaAsync()
        {

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                ErrorMessage = "Por favor ingresa el nombre de la película.";
                return;
            }

            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                string apiUrl = "https://www.freetestapi.com/api/v1/movies"; // Reemplaza con la URL real
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();

                    // Deserializar el JSON a una lista de películas
                    var peliculas = JsonSerializer.Deserialize<List<Peliculas>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // Ignorar mayúsculas/minúsculas en los nombres de propiedades
                    });

                    var peliculaEncontrada = peliculas?.FirstOrDefault(p =>
                        p.title.Equals(SearchText, StringComparison.OrdinalIgnoreCase));

                    if (peliculaEncontrada != null)
                    {
                        Peliculas.Clear();
                        Peliculas.Add(peliculaEncontrada);
                        SelectedPelicula = peliculaEncontrada;
                    }
                    else
                    {
                        ErrorMessage = "Película no encontrada.";
                        Peliculas.Clear();
                    }
                }
                else
                {
                    ErrorMessage = $"Error al conectar con la API: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al conectar con la API: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
