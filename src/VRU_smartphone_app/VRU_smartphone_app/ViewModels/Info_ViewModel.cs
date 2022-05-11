using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace VRU_smartphone_app.ViewModels
{
    public class Info_ViewModel : BaseViewModel
    {
        public Info_ViewModel()
        {

        }

        private int ID_ = 0;
        public int ID
        {
            get { return ID_; }
            set { SetProperty(ref ID_, value); }
        }

        private string Name_ = "";
        public string Name
        {
            get { return Name_; }
            set { SetProperty(ref Name_, value); }
        }

        private string Vehicle_ = "";
        public string Vehicle
        {
            get { return Vehicle_; }
            set { SetProperty(ref Vehicle_, value); }
        }

        private int License_Plate_ = 0;
        public int License_Plate
        {
            get { return License_Plate_; }
            set { SetProperty(ref License_Plate_, value); }
        }

        private double Latitude_ = 0;
        public double Latitude
        {
            get { return Latitude_; }
            set { SetProperty(ref Latitude_, value); }
        }

        private double Longitude_ = 0;
        public double Longitude
        {
            get { return Longitude_; }
            set { SetProperty(ref Longitude_, value); }
        }

        private double Speed_ = 0;
        public double Speed
        {
            get { return Speed_; }
            set { SetProperty(ref Speed_, value); }
        }

        private bool Collision_ = false;
        public bool Collision
        {
            get { return Collision_; }
            set { SetProperty(ref Collision_, value); }
        }

        private int Collision_Type_ = 0;
        public int Collision_Type
        {
            get { return Collision_Type_; }
            set { SetProperty(ref Collision_Type_, value); }
        }

        private string Collision_With_ = "";
        public string Collision_With
        {
            get { return Collision_With_; }
            set { SetProperty(ref Collision_With_, value); }
        }

        private bool Camera_Detected_ = false;
        public bool Camera_Detected
        {
            get { return Camera_Detected_; }
            set { SetProperty(ref Camera_Detected_, value); }
        }

        private double Camera_Certainty_ = 0;
        public double Camera_Certainty
        {
            get { return Camera_Certainty_; }
            set { SetProperty(ref Camera_Certainty_, value); }
        }

        private Brush Camera_Status_ = Brush.White;
        public Brush Camera_Status
        {
            get { return Camera_Status_; }
            set { SetProperty(ref Camera_Status_, value); }
        }

        private string Camera_Data_ = "";
        public string Camera_Data
        {
            get { return Camera_Data_; }
            set { SetProperty(ref Camera_Data_, value); }
        }

        private string UltraSonic_Data_ = "";
        public string UltraSonic_Data
        {
            get { return UltraSonic_Data_; }
            set { SetProperty(ref UltraSonic_Data_, value); }
        }

        private double UltraSonic_Range_ = 0;
        public double UltraSonic_Range
        {
            get { return UltraSonic_Range_; }
            set { SetProperty(ref UltraSonic_Range_, value); }
        }

        private Brush UltraSonic_Status_ = Brush.White;
        public Brush UltraSonic_Status
        {
            get { return UltraSonic_Status_; }
            set { SetProperty(ref UltraSonic_Status_, value); }
        }

        private string GPS_Data_ = "";
        public string GPS_Data
        {
            get { return GPS_Data_; }
            set { SetProperty(ref GPS_Data_, value); }
        }

        private double GPS_Range_ = 0;
        public double GPS_Range
        {
            get { return GPS_Range_; }
            set { SetProperty(ref GPS_Range_, value); }
        }

        private Brush GPS_Status_ = Brush.White;
        public Brush GPS_Status
        {
            get { return GPS_Status_; }
            set { SetProperty(ref GPS_Status_, value); }
        }

        private string Server_Response_String_ = "";
        public string Server_Response_String
        {
            get { return Server_Response_String_; }
            set { SetProperty(ref Server_Response_String_, value); }
        }
    }
}
