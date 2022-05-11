using System;
using System.Collections.Generic;
using System.Text;

namespace VRU_smartphone_app.ViewModels
{
    public class Locations_ViewModel : BaseViewModel
    {
        public Locations_ViewModel()
        {

        }

        private double Latitude_ = 43.657;
        public double Latitude
        {
            get { return Latitude_; }
            set { SetProperty(ref Latitude_, value); }
        }

        private double Longitude_ = -79.377;
        public double Longitude
        {
            get { return Longitude_; }
            set { SetProperty(ref Longitude_, value); }
        }

        private string Location_ = "";
        public string Location
        {
            get { return Location_; }
            set { SetProperty(ref Location_, value); }
        }

        private double Speed_ = 0;
        public double Speed
        {
            get { return Speed_; }
            set { SetProperty(ref Speed_, value); }
        }
    }
}
