using System;
using VRU_smartphone_app.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VRU_smartphone_app
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
