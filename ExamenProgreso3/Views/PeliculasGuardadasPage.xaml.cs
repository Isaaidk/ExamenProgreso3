using ExamenProgreso3.ViewModel;
using System;

namespace ExamenProgreso3.Views
{
    public partial class PeliculasGuardadasPage : ContentPage
    {
        public PeliculasGuardadasPage()
        {
            InitializeComponent();

           
            BindingContext = new PeliculasGuardadasViewModel();
        }

     
    }
}
