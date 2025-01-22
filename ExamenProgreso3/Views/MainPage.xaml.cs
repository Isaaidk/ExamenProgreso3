
using ExamenProgreso3.ViewModel;


namespace ExamenProgreso3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

           
            BindingContext = new PeliculasViewModel();
        }
    }
}