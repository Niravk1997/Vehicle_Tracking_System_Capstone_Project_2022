using MediaManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VRU_smartphone_app.Classes;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VRU_smartphone_app.Views
{
    public partial class Info : TabbedPage
    {
        private System.Timers.Timer Check_Server_Connected_Timer;
        private System.Timers.Timer Server_Communication;
        private System.Timers.Timer Update_Location_and_Speed_Timer;
        private System.Timers.Timer Warning_Sound_Timer;

        private HttpClientHandler HTTPS_Handler;
        private HttpClient Server_Connect;
        private StringContent Server_POST_Data;
        private HttpResponseMessage Server_Response;

        private Location Location_Data;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private GeolocationRequest geolocationRequest = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

        private bool is_Warning_Playing = false;

        public Info()
        {
            InitializeComponent();
            Initialize_Timers();
            Initialize_Server_Objects();
        }

        private void Initialize_Server_Objects()
        {
            HTTPS_Handler = new HttpClientHandler();
            HTTPS_Handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            Server_Connect = new HttpClient(HTTPS_Handler);
        }

        private void Initialize_Timers()
        {
            Check_Server_Connected_Timer = new System.Timers.Timer();
            Check_Server_Connected_Timer.Elapsed += Check_Server_Connected;
            Check_Server_Connected_Timer.Interval = 1000; // milliseconds
            Check_Server_Connected_Timer.AutoReset = true;
            Check_Server_Connected_Timer.Enabled = true;

            Server_Communication = new System.Timers.Timer();
            Server_Communication.Elapsed += Server_Communication_Data_GET_POST;
            Server_Communication.Interval = 50; // milliseconds
            Server_Communication.AutoReset = false;
            Server_Communication.Enabled = false;

            Update_Location_and_Speed_Timer = new System.Timers.Timer();
            Update_Location_and_Speed_Timer.Elapsed += Update_Location_and_Speed_Data;
            Update_Location_and_Speed_Timer.Interval = 50; // milliseconds
            Update_Location_and_Speed_Timer.AutoReset = false;
            Update_Location_and_Speed_Timer.Enabled = true;

            Warning_Sound_Timer = new System.Timers.Timer();
            Warning_Sound_Timer.Elapsed += Play_Warning_Sound;
            Warning_Sound_Timer.Interval = 100; // milliseconds
            Warning_Sound_Timer.AutoReset = false;
            Warning_Sound_Timer.Enabled = false;
        }

        private  void Play_Warning_Sound(object sender, System.Timers.ElapsedEventArgs e) 
        {
            is_Warning_Playing = true;
            CrossMediaManager.Current.Play("file:///android_asset/beep.mp3");
            Thread.Sleep(500);
            is_Warning_Playing = false;
        }

        private void Check_Server_Connected(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Global_Variables.isConnected)
            {
                Server_Communication.Enabled = true;
                Check_Server_Connected_Timer.Stop();
                Check_Server_Connected_Timer.Dispose();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        var response = await Server_Connect.GetAsync("https://" + Global_Variables.Server_IP + "/VRU_User_GET/" + Global_Variables.ID);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            GET_Full_Data_JSON Full_Data = JsonConvert.DeserializeObject<GET_Full_Data_JSON>(content);

                            Global_Variables.Name = Full_Data.Name;
                            Global_Variables.Vehicle = Full_Data.Vehicle;
                            Global_Variables.License_Plate = Full_Data.License_Plate;

                            Info_VM.Name = Full_Data.Name;
                            Info_VM.Vehicle = Full_Data.Vehicle;
                            Info_VM.License_Plate = Full_Data.License_Plate;
                        }
                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }

        private void Server_Communication_Data_GET_POST(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Get_Mobile_Essentials_Data_from_Server();
                Update_Data_to_Server();
                Server_Communication.Enabled = true;

                if(Global_Variables.UltraSonic_Range < 30 & Global_Variables.UltraSonic_Range != 0) 
                {
                    if (Warning_Sound_Timer.Enabled == false & is_Warning_Playing == false) 
                    {
                        Warning_Sound_Timer.Enabled = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                Info_VM.Server_Response_String = Ex.Message;
            }
        }

        private void Update_Location_and_Speed_Data(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        Location_Data = await Geolocation.GetLocationAsync(geolocationRequest, cts.Token);
                        if (Location_Data != null)
                        {
                            Info_VM.Latitude = Location_Data.Latitude;
                            Info_VM.Longitude = Location_Data.Longitude;
                            Global_Variables.Latitude = Location_Data.Latitude;
                            Global_Variables.Longitude = Location_Data.Longitude;
                            Info_VM.Speed = (double)Location_Data.Speed;
                            Global_Variables.Speed = (double)Location_Data.Speed;
                        }

                        Update_Location_and_Speed_Timer.Enabled = true;
                    }
                    catch (Exception)
                    {
                        Update_Location_and_Speed_Timer.Enabled = true;
                    }
                });
            }
            catch (Exception)
            {

            }
        }

        private async void Update_Data_to_Server()
        {
            string Initial_Setup_JSON = JsonConvert.SerializeObject(new Update_Data_JSON(Global_Variables.Latitude, Global_Variables.Longitude, Global_Variables.Speed, Global_Variables.Car_Movement));
            Server_POST_Data = new StringContent(Initial_Setup_JSON, Encoding.UTF8, "application/json");
            Server_Response = await Server_Connect.PostAsync("https://" + Global_Variables.Server_IP + "/VRU_User_POST/Update/" + Global_Variables.ID, Server_POST_Data);
            Info_VM.Server_Response_String = await Server_Response.Content.ReadAsStringAsync();
        }

        private async void Get_Mobile_Essentials_Data_from_Server()
        {
            var response = await Server_Connect.GetAsync("https://" + Global_Variables.Server_IP + "/VRU_User_GET/Essentials_Mobile/" + Global_Variables.ID);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                GET_Essentials_Mobile_Data_JSON Mobile_Essentials_Data = JsonConvert.DeserializeObject<GET_Essentials_Mobile_Data_JSON>(content);

                Global_Variables.isCollision = Mobile_Essentials_Data.isCollision;
                Global_Variables.Collision_Type = Mobile_Essentials_Data.Collision_Type;
                Global_Variables.Collision_With = Mobile_Essentials_Data.Collision_With;

                Global_Variables.isCamera_Detected = Mobile_Essentials_Data.isCamera_Detected;
                Global_Variables.Camera_Data = Mobile_Essentials_Data.Camera_Data;

                Global_Variables.UltraSonic_Data = Mobile_Essentials_Data.UltraSonic_Data;
                Global_Variables.UltraSonic_Range = Mobile_Essentials_Data.UltraSonic_Range;

                Global_Variables.GPS_Data = Mobile_Essentials_Data.GPS_Data;
                Global_Variables.GPS_Range = Mobile_Essentials_Data.GPS_Range;

                Info_VM.Collision = Mobile_Essentials_Data.isCollision;
                Info_VM.Collision_Type = Mobile_Essentials_Data.Collision_Type;
                Info_VM.Collision_With = Mobile_Essentials_Data.Collision_With;

                Info_VM.Camera_Detected = Mobile_Essentials_Data.isCamera_Detected;
                Info_VM.Camera_Data = Mobile_Essentials_Data.Camera_Data;
                Info_VM.Camera_Certainty = Mobile_Essentials_Data.Camera_Certainty;

                Info_VM.UltraSonic_Data = Mobile_Essentials_Data.UltraSonic_Data;
                Info_VM.UltraSonic_Range = Mobile_Essentials_Data.UltraSonic_Range;

                Info_VM.GPS_Data = Mobile_Essentials_Data.GPS_Data;
                Info_VM.GPS_Range = Mobile_Essentials_Data.GPS_Range;

                if (Global_Variables.UltraSonic_Range < 30 & Global_Variables.UltraSonic_Range != 0)
                {
                    Info_VM.UltraSonic_Status = Brush.Red;
                }
                else if (Global_Variables.UltraSonic_Range < 50 & Global_Variables.UltraSonic_Range > 30)
                {
                    Info_VM.UltraSonic_Status = Brush.Yellow;
                }
                else
                {
                    Info_VM.UltraSonic_Status = Brush.LimeGreen;
                }

                if (Global_Variables.GPS_Range < 50)
                {
                    Info_VM.GPS_Status = Brush.Orange;
                }
                else if (Global_Variables.UltraSonic_Range < 100)
                {
                    Info_VM.GPS_Status = Brush.Yellow;
                }
                else
                {
                    Info_VM.GPS_Status = Brush.LimeGreen;
                }

                if (Global_Variables.isCamera_Detected == true)
                {
                    if (Mobile_Essentials_Data.Camera_Certainty > 70)
                    {
                        Info_VM.Camera_Status = Brush.Red;
                    }
                    else 
                    {
                        Info_VM.Camera_Status = Brush.Orange;
                    }
                }
                else 
                {
                    Info_VM.Camera_Status = Brush.LimeGreen;
                }
            }
        }

        private void Get_Data_from_Server()
        {

        }

        private void Forward_Clicked(object sender, EventArgs e)
        {
            Global_Variables.Car_Movement = 1;
        }

        private void Left_Clicked(object sender, EventArgs e)
        {
            Global_Variables.Car_Movement = 3;
        }

        private void Stop_Clicked(object sender, EventArgs e)
        {
            Global_Variables.Car_Movement = 0;
        }

        private void Right_Clicked(object sender, EventArgs e)
        {
            Global_Variables.Car_Movement = 4;
        }

        private void Backward_Clicked(object sender, EventArgs e)
        {
            Global_Variables.Car_Movement = 2;
        }
    }
}