using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VRU_smartphone_app.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VRU_smartphone_app.Views
{
    public partial class Config : ContentPage
    {

        public Config()
        {
            InitializeComponent();
        }

        private async void Initial_Server_Setup()
        {
            HttpClientHandler ssl_Handler = new HttpClientHandler();
            ssl_Handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            InitialSetup_JSON Initial_Setup = new InitialSetup_JSON(true, Config_VM.VRU_Name, Config_VM.VRU_Vehicle_Type, Config_VM.VRU_Vehicle_License_Plate);
            string Initial_Setup_JSON = JsonConvert.SerializeObject(Initial_Setup);

            HttpClient Server_Connect = new HttpClient(ssl_Handler);

            StringContent JSON_Content = new StringContent(Initial_Setup_JSON, Encoding.UTF8, "application/json");
            HttpResponseMessage Server_Response = await Server_Connect.PostAsync("https://" + Config_VM.Server_URL + "/VRU_User_POST/Setup/" + Config_VM.Server_ID, JSON_Content);
            string Server_Message = await Server_Response.Content.ReadAsStringAsync();

            Config_VM.Server_Response_Message = Server_Message;

            Global_Variables.Server_IP = Config_VM.Server_URL;
            Global_Variables.ID = Config_VM.Server_ID;
            Global_Variables.Name = Config_VM.VRU_Name;
            Global_Variables.Vehicle = Config_VM.VRU_Vehicle_Type;
            Global_Variables.License_Plate = Config_VM.VRU_Vehicle_License_Plate;
            Global_Variables.isConnected = true;
        }

        private void Server_Connect_Clicked(object sender, EventArgs e)
        {
            if (Config_VM.Server_ID <= 2 & Config_VM.Server_ID >= 0)
            {
                if (Config_VM.VRU_Name != String.Empty || Config_VM.VRU_Name != "")
                {
                    if (Config_VM.VRU_Vehicle_Type != String.Empty || Config_VM.VRU_Vehicle_Type != "")
                    {
                        if (Config_VM.VRU_Vehicle_License_Plate >= 1000 & Config_VM.VRU_Vehicle_License_Plate <= 9999)
                        {
                            try
                            {
                                Initial_Server_Setup();
                                Config_VM.Server_Status = "Connected to Server.";
                                Config_VM.isServer_not_Connected = false;
                            }
                            catch (Exception Ex)
                            {
                                Config_VM.Server_Status = Ex.Message;
                            }
                        }
                        else
                        {
                            Config_VM.Server_Status = "Not Connected: License Plate must be between 1000 and 9999.";
                        }
                    }
                    else
                    {
                        Config_VM.Server_Status = "Not Connected: Vehicle Type must not be empty.";
                    }
                }
                else
                {
                    Config_VM.Server_Status = "Not Connected: Name must not be empty.";
                }
            }
            else
            {
                Config_VM.Server_Status = "Not Connected: ID must be between 0 and 2.";
            }
        }
    }
}