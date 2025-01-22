using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using ExamenProgreso3.Models;

namespace ExamenProgreso3.Services
{
    public class DatabaseService
    {
        private SQLiteConnection _connection;

        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "IsaacPugaPeliculas.db");
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<BDPeliculas>();
        }

        // Método para guardar una película
        public void SavePelicula(BDPeliculas pelicula)
        {
            try
            {
                var existingPelicula = _connection.Table<BDPeliculas>().FirstOrDefault(p => p.title == pelicula.title);
                if (existingPelicula == null)
                {
                    _connection.Insert(pelicula);
                }
                else
                {
                    existingPelicula.genre = pelicula.genre;
                    existingPelicula.actors = pelicula.actors;
                    existingPelicula.awards = pelicula.awards;
                    existingPelicula.website = pelicula.website;
                    _connection.Update(existingPelicula);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando película: {ex.Message}");
            }
        }

        // Método para obtener todas las películas de la base de datos
        public List<BDPeliculas> GetAllPeliculas()
        {
            return _connection.Table<BDPeliculas>().ToList();
        }
    }
}
