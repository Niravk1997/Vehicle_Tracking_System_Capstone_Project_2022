#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>
#include <Arduino_JSON.h>

const char* ssid = "Nx97";
const char* password = "l502xxxx";

const char* http_Post_Get_Request_serverName = "http://192.168.0.57:3443/VRU_User_POST/Update_UltraSonic/1";

uint8_t Motor_A_1 = D0;
uint8_t Motor_A_2 = D1;

uint8_t Motor_B_1 = D2;
uint8_t Motor_B_2 = D3;

const int trigPin = 15;
const int echoPin = 13;

//define sound velocity in cm/uS
#define SOUND_VELOCITY 0.034
#define CM_TO_INCH 0.393701

long duration;
int UltraSonic_Sensor_distance_CM;

WiFiClient client;
HTTPClient http;

void setup() {
  Set_PinMode();
  Tank_Stop();
  delay(1000);

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
  }

  http.begin(client, http_Post_Get_Request_serverName);
  http.addHeader("Content-Type", "application/json");
}

void loop() {
  Update_UltraSonic_Sensor_Reading();
  //Check WiFi connection status
  http_Post_Get_Request(http_Post_Get_Request_serverName);
}

void http_Post_Get_Request(const char* serverName) {
  http.begin(client, serverName);
  http.addHeader("Content-Type", "application/json");

  JSONVar UltraSonic_Range_Object;
  UltraSonic_Range_Object["UltraSonic_Range"] = UltraSonic_Sensor_distance_CM;
  String JSON_Data = JSON.stringify(UltraSonic_Range_Object);
  int httpResponseCode = http.POST(JSON_Data);

  if (httpResponseCode > 0) {
    String payload = http.getString();
    Parse_Car_Movement_JSON(payload);
  }
  http.end();
}

void Update_UltraSonic_Sensor_Reading() {
  // Clears the trigPin
  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);
  // Sets the trigPin on HIGH state for 10 micro seconds
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);

  // Reads the echoPin, returns the sound wave travel time in microseconds
  duration = pulseIn(echoPin, HIGH);

  // Calculate the distance
  UltraSonic_Sensor_distance_CM = (int)( duration * SOUND_VELOCITY / 2);

  if (UltraSonic_Sensor_distance_CM > 170) {
    UltraSonic_Sensor_distance_CM = 170;
  }

  if (UltraSonic_Sensor_distance_CM < 10) {
    Tank_Stop();
    delay(1000);
    Tank_Backward();
    delay(1000);
    Tank_Stop();
  }
}

void Parse_Car_Movement_JSON(String JSON_Data) {
  JSONVar myObject = JSON.parse(JSON_Data);
  if (JSON.typeof(myObject) != "undefined") {
    JSONVar keys = myObject.keys();
    JSONVar Car_Movement = myObject[keys[0]];
    if (int(Car_Movement) == 1 & UltraSonic_Sensor_distance_CM > 10) {
      Tank_Forward();
    }
    else if (int(Car_Movement) == 2) {
      Tank_Backward();
    }
    else if (int(Car_Movement) == 3 & UltraSonic_Sensor_distance_CM > 10) {
      Tank_Left();
    }
    else if (int(Car_Movement) == 4 & UltraSonic_Sensor_distance_CM > 10) {
      Tank_Right();
    }
    else {
      Tank_Stop();
    }
  }
}

void Set_PinMode() {
  pinMode(trigPin, OUTPUT); // Sets the trigPin as an Output
  pinMode(echoPin, INPUT); // Sets the echoPin as an Input

  pinMode(Motor_A_1, OUTPUT);
  pinMode(Motor_A_2, OUTPUT);
  pinMode(Motor_B_1, OUTPUT);
  pinMode(Motor_B_2, OUTPUT);
}

void Tank_Stop() {
  digitalWrite(Motor_A_1, LOW);
  digitalWrite(Motor_A_2, LOW);
  digitalWrite(Motor_B_1, LOW);
  digitalWrite(Motor_B_2, LOW);
}

void Tank_Left() {
  digitalWrite(Motor_A_1, HIGH);
  digitalWrite(Motor_A_2, LOW);
  digitalWrite(Motor_B_1, HIGH);
  digitalWrite(Motor_B_2, LOW);
}

void Tank_Right() {
  digitalWrite(Motor_A_1, LOW);
  digitalWrite(Motor_A_2, HIGH);
  digitalWrite(Motor_B_1, LOW);
  digitalWrite(Motor_B_2, HIGH);
}

void Tank_Backward() {
  digitalWrite(Motor_A_1, HIGH);
  digitalWrite(Motor_A_2, LOW);
  digitalWrite(Motor_B_1, LOW);
  digitalWrite(Motor_B_2, HIGH);
}

void Tank_Forward() {
  digitalWrite(Motor_A_1, LOW);
  digitalWrite(Motor_A_2, HIGH);
  digitalWrite(Motor_B_1, HIGH);
  digitalWrite(Motor_B_2, LOW);
}
