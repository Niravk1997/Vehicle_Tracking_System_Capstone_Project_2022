using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VRU_smartphone_app.Classes
{
    public class InitialSetup_JSON
    {
        public InitialSetup_JSON(bool IsConnected, string Name, string Vehicle, int LicensePlate)
        {
            this.IsConnected = IsConnected;
            this.Name = Name;
            this.Vehicle = Vehicle;
            this.LicensePlate = LicensePlate;
        }

        [JsonProperty("isConnected")]
        public bool IsConnected { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Vehicle")]
        public string Vehicle { get; set; }

        [JsonProperty("License_Plate")]
        public int LicensePlate { get; set; }
    }

    public class Update_Data_JSON
    {
        public Update_Data_JSON(double Latitude, double Longitude, double Speed, double Car_Movement)
        {
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Speed = Speed;
            this.Car_Movement = Car_Movement;
        }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("Speed")]
        public double Speed { get; set; }

        [JsonProperty("Car_Movement")]
        public double Car_Movement { get; set; }
    }

    public class GET_Full_Data_JSON
    {
        public GET_Full_Data_JSON(int ID, bool isConnected, string Name, string Vehicle,
            int License_Plate, double Latitude, double Longitude, double Speed,
            bool isCollision, int Collision_Type, string Collision_With, bool isCamera_Detected,
            string Camera_Data, string UltraSonic_Data, double UltraSonic_Range)
        {
            this.ID = ID;
            this.isConnected = isConnected;
            this.Name = Name;
            this.Vehicle = Vehicle;
            this.License_Plate = License_Plate;

            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Speed = Speed;

            this.isCollision = isCollision;
            this.Collision_Type = Collision_Type;
            this.Collision_With = Collision_With;

            this.isCamera_Detected = isCamera_Detected;
            this.Camera_Data = Camera_Data;

            this.UltraSonic_Data = UltraSonic_Data;
            this.UltraSonic_Range = UltraSonic_Range;
        }

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("isConnected")]
        public bool isConnected { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Vehicle")]
        public string Vehicle { get; set; }

        [JsonProperty("License_Plate")]
        public int License_Plate { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("Speed")]
        public double Speed { get; set; }

        [JsonProperty("isCollision")]
        public bool isCollision { get; set; }

        [JsonProperty("Collision_Type")]
        public int Collision_Type { get; set; }

        [JsonProperty("Collision_With")]
        public string Collision_With { get; set; }

        [JsonProperty("isCamera_Detected")]
        public bool isCamera_Detected { get; set; }

        [JsonProperty("Camera_Data")]
        public string Camera_Data { get; set; }

        [JsonProperty("UltraSonic_Data")]
        public string UltraSonic_Data { get; set; }

        [JsonProperty("UltraSonic_Range")]
        public double UltraSonic_Range { get; set; }
    }

    public class GET_Essentials_Mobile_Data_JSON
    {
        public GET_Essentials_Mobile_Data_JSON(bool isCollision, int Collision_Type, string Collision_With, bool isCamera_Detected,
            string Camera_Data, double Camera_Certainty, string UltraSonic_Data, double UltraSonic_Range, string GPS_Data, double GPS_Range)
        {
            this.isCollision = isCollision;
            this.Collision_Type = Collision_Type;
            this.Collision_With = Collision_With;

            this.isCamera_Detected = isCamera_Detected;
            this.Camera_Data = Camera_Data;
            this.Camera_Certainty = Camera_Certainty;

            this.UltraSonic_Data = UltraSonic_Data;
            this.UltraSonic_Range = UltraSonic_Range;

            this.GPS_Data = GPS_Data;
            this.GPS_Range = GPS_Range;
        }

        [JsonProperty("isCollision")]
        public bool isCollision { get; set; }

        [JsonProperty("Collision_Type")]
        public int Collision_Type { get; set; }

        [JsonProperty("Collision_With")]
        public string Collision_With { get; set; }

        [JsonProperty("isCamera_Detected")]
        public bool isCamera_Detected { get; set; }

        [JsonProperty("Camera_Data")]
        public string Camera_Data { get; set; }

        [JsonProperty("Camera_Certainty")]
        public double Camera_Certainty { get; set; }

        [JsonProperty("UltraSonic_Data")]
        public string UltraSonic_Data { get; set; }

        [JsonProperty("UltraSonic_Range")]
        public double UltraSonic_Range { get; set; }

        [JsonProperty("GPS_Data")]
        public string GPS_Data { get; set; }

        [JsonProperty("GPS_Range")]
        public double GPS_Range { get; set; }
    }
}
