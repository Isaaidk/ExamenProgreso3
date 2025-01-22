using ExamenProgreso3.Models;
using ExamenProgreso3.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExamenProgreso3.ViewModel
{
    public class PeliculasGuardadasViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        // Lista observable para mostrar las películas
        public ObservableCollection<BDPeliculas> PeliculasGuardadas { get; set; }

        public PeliculasGuardadasViewModel()
        {
            _databaseService = new DatabaseService();
            PeliculasGuardadas = new ObservableCollection<BDPeliculas>(_databaseService.GetAllPeliculas());
        }
    }
}
