using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VRU_smartphone_app.ViewModels
{
    public class Config_ViewModel : BaseViewModel
    {
        public Config_ViewModel()
        {

        }

        private bool isServer_Connected_ = false;
        public bool isServer_Connected
        {
            get { return isServer_Connected_; }
            set { SetProperty(ref isServer_Connected_, value); }
        }

        private string Server_URL_ = "192.168.0.221:5000";
        public string Server_URL
        {
            get { return Server_URL_; }
            set { SetProperty(ref Server_URL_, value); }
        }

        private string Server_Response_Message_ = "";
        public string Server_Response_Message
        {
            get { return Server_Response_Message_; }
            set { SetProperty(ref Server_Response_Message_, value); }
        }

        private int Server_ID_ = 2;
        public int Server_ID
        {
            get { return Server_ID_; }
            set { SetProperty(ref Server_ID_, value); }
        }

        private string VRU_Name_ = "Quang Dinh";
        public string VRU_Name
        {
            get { return VRU_Name_; }
            set { SetProperty(ref VRU_Name_, value); }
        }

        private string VRU_Vehicle_Type_ = "Pedestrian";
        public string VRU_Vehicle_Type
        {
            get { return VRU_Vehicle_Type_; }
            set { SetProperty(ref VRU_Vehicle_Type_, value); }
        }

        private int VRU_Vehicle_License_Plate_ = 1234;
        public int VRU_Vehicle_License_Plate
        {
            get { return VRU_Vehicle_License_Plate_; }
            set { SetProperty(ref VRU_Vehicle_License_Plate_, value); }
        }

        private bool isServer_not_Connected_ = true;
        public bool isServer_not_Connected
        {
            get { return isServer_not_Connected_; }
            set { SetProperty(ref isServer_not_Connected_, value); }
        }

        private string Server_Status_ = "Not Connected.";
        public string Server_Status
        {
            get { return Server_Status_; }
            set { SetProperty(ref Server_Status_, value); }
        }
    }
}