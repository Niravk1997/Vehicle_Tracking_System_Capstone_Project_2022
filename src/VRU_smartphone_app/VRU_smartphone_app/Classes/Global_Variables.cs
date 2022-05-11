using System;
using System.Collections.Generic;
using System.Text;

namespace VRU_smartphone_app.Classes
{
    public static class Global_Variables
    {
        public static string Server_IP = "";

        public static bool isConnected = false;

        public static int ID = 0;
        public static string Name = "";
        public static string Vehicle = "";
        public static int License_Plate = 0;

        public static double Latitude = 0;
        public static double Longitude = 0;
        public static double Speed = 0;

        public static bool isCollision = false;
        public static int Collision_Type = 0;
        public static string Collision_With = "";
        public static bool isCamera_Detected = false;
        public static string Camera_Data = "";

        public static string UltraSonic_Data = "";
        public static double UltraSonic_Range = 0;

        public static string GPS_Data = "";
        public static double GPS_Range = 0;

        //Car Movement: 0 = Stop, 1 = Forward, 2 = Backward, 3 = Left Turn, 4 = Right Turn
        public static double Car_Movement = 0;
    }
}
