
    using ExamenProgreso3.ViewModel;
namespace ExamenProgreso3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Establecer el contexto de datos al ViewModel
            BindingContext = new PeliculasViewModel();
        }
    }
}