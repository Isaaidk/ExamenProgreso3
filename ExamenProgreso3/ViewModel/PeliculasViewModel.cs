using ExamenProgreso3.Models;
using ExamenProgreso3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExamenProgreso3.ViewModel
{
    public class PeliculasViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly DatabaseService _databaseService;

        public ObservableCollection<BDPeliculas> Peliculas { get; set; } = new ObservableCollection<BDPeliculas>();
        private string _searchText;
        private BDPeliculas _selectedPelicula;
        private string _errorMessage;
        private bool _isLoading;

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public BDPeliculas SelectedPelicula
        {
            get => _selectedPelicula;
            set => SetProperty(ref _selectedPelicula, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        // Comandos
        public ICommand BuscarPeliculaCommand { get; }
        public ICommand GuardarPeliculaCommand { get; }

        public PeliculasViewModel()
        {
            _httpClient = new HttpClient();
            _databaseService = new DatabaseService();
            BuscarPeliculaCommand = new Command(async () => await BuscarPeliculaAsync());
            GuardarPeliculaCommand = new Command(SavePeliculaToDatabase);
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
                string apiUrl = "https://www.freetestapi.com/api/v1/movies";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonString); // Para ver cómo llega el JSON

                    var peliculas = JsonSerializer.Deserialize<List<BDPeliculas>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    var peliculaEncontrada = peliculas?.FirstOrDefault(p =>
                        p.title.Equals(SearchText, StringComparison.OrdinalIgnoreCase));

                    if (peliculaEncontrada != null)
                    {
                        Peliculas.Clear();
                        Peliculas.Add(peliculaEncontrada);
                        SelectedPelicula = peliculaEncontrada;

                        // Guardar la película en la base de datos
                        _databaseService.SavePelicula(peliculaEncontrada);
                    }
                    else
                    {
                        ErrorMessage = "Película no encontrada.";
                        Peliculas.Clear();
                        SelectedPelicula = null;
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

        // Método para guardar la película en la base de datos
        private void SavePeliculaToDatabase()
        {
            if (SelectedPelicula != null)
            {
                _databaseService.SavePelicula(SelectedPelicula);
                ErrorMessage = "Película guardada correctamente.";
            }
            else
            {
                ErrorMessage = "No hay ninguna película seleccionada para guardar.";
            }
        }
    }
}
