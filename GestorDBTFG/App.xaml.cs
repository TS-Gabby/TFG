using GestorDBTFG.View;

namespace GestorDBTFG
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);
            if (window != null)
            {
                window.Title = "GestorBD (App Servidor)";
            }

            return window;
        }
    }
}
