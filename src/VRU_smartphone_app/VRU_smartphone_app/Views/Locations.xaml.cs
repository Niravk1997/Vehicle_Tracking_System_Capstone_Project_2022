using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VRU_smartphone_app.ViewModels;
using VRU_smartphone_app.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace VRU_smartphone_app.Views
{
    public partial class Locations_Page : ContentPage
    {
        public Locations_Page()
        {
            InitializeComponent(); 
            Set_Initial_Map_Location();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void Set_Initial_Map_Location() 
        {
            Position position = new Position(43.65755900568808, -79.37720314328449);
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.444));
            GMap.MoveToRegion(mapSpan);
            GMap.TrafficEnabled = true;
            Add_Clear_Pin_A(position.Latitude, position.Longitude);
        }

        private void Add_Clear_Pin_A(double Longitude, double Latitude)
        {
            Pin pin_1 = new Pin
            {
                Label = "User",
                Address = "Ryerson University",
                Type = PinType.Generic,
                Position = new Position(Longitude, Latitude)
            };
            GMap.Pins.Add(pin_1);
        }
    }
}