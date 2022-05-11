//ID: 0  is Vehicle A
//ID: 1 is Vehicle B
//ID: 2 is Pedestrian
//Collision_With: Name, Vehicle Type, License Plate, Latitude, Longitude, Speed

//Car Movement: 0 = Stop, 1 = Forward, 2 = Backward, 3 = Left Turn, 4 = Right Turn
let VRU_Users = [
    {
        ID: 0,
        isConnected: false,
        Name: '',
        Vehicle:'',
        License_Plate: 0,
        Latitude: 0,
        Longitude: 0,
        Speed: 0,
        isCollision: false,
        Collision_Type: 0,
        Collision_With: '',
        isCamera_Detected: false,
        Camera_Certainty: 0,
        Camera_Data: '',
        UltraSonic_Data: '',
        UltraSonic_Range: 0,
        GPS_Data: '',
        GPS_Range: 0,
        Car_Movement: 0,
        Auto_Stop: false
    },
    {
        ID: 1,
        isConnected: false,
        Name: '',
        Vehicle:'',
        License_Plate: 0,
        Latitude: 0,
        Longitude: 0,
        Speed: 0,
        isCollision: false,
        Collision_Type: 0,
        Collision_With: '',
        isCamera_Detected: false,
        Camera_Certainty: 0,
        Camera_Data: '',
        UltraSonic_Data: '',
        UltraSonic_Range: 0,
        GPS_Data: '',
        GPS_Range: 0,
        Car_Movement: 0,
        Auto_Stop: false
    },
    {
        ID: 2,
        isConnected: false,
        Name: '',
        Vehicle:'',
        License_Plate: 0,
        Latitude: 0,
        Longitude: 0,
        Speed: 0,
        isCollision: false,
        Collision_Type: 0,
        Collision_With: '',
        isCamera_Detected: false,
        Camera_Certainty: 0,
        Camera_Data: '',
        UltraSonic_Data: '',
        UltraSonic_Range: 0,
        GPS_Data: '',
        GPS_Range: 0,
        Car_Movement: 0,
        Auto_Stop: false
    }
];

module.exports = VRU_Users;