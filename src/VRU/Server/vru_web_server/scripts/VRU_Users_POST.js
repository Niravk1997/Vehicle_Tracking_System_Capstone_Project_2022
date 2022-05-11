const express = require('express');
const VRU_Users = require('./Data');
const router = express.Router();

router.post('/Setup/:ID', (req, res) => {
    const VRU_User_ID = parseInt(req.params.ID);
    if(VRU_User_ID >= 0 & VRU_User_ID <= 2) {
        
        VRU_Users[VRU_User_ID].Name = req.body.Name;
        VRU_Users[VRU_User_ID].License_Plate = parseInt(req.body.License_Plate);
        VRU_Users[VRU_User_ID].Vehicle = req.body.Vehicle;
        VRU_Users[VRU_User_ID].isConnected = req.body.isConnected;

        res.json({VRU_User: "Accepted", User_Name: VRU_Users[VRU_User_ID].Name});
        console.log(req.body);
    }
    else
    {
        res.send({User_Name: "Unknown", Status: "ERROR"});
    }
});

router.post('/Update/:ID', (req, res) => {
    const VRU_User_ID = parseInt(req.params.ID);
    if(VRU_User_ID >= 0 & VRU_User_ID <= 2) {
        
        VRU_Users[VRU_User_ID].Latitude = parseFloat(req.body.Latitude);
        VRU_Users[VRU_User_ID].Longitude = parseFloat(req.body.Longitude);
        VRU_Users[VRU_User_ID].Speed = parseFloat(req.body.Speed);
        VRU_Users[VRU_User_ID].Car_Movement = parseFloat(req.body.Car_Movement);

        Update_GPS_Minimum_Distance();

        res.json({User_Name: VRU_Users[VRU_User_ID].Name, Status: "Data Updated"});
    }
    else
    {
        res.send({User_Name: "Unknown", Status: "ERROR"});
    }
});

router.post('/Update_Camera/:ID', (req, res) => {
    const VRU_User_ID = parseInt(req.params.ID);
    if(VRU_User_ID >= 0 & VRU_User_ID <= 2) {
        
        VRU_Users[VRU_User_ID].isCamera_Detected = req.body.isCamera_Detected;
        VRU_Users[VRU_User_ID].Camera_Certainty = req.body.Camera_Certainty;

        if (VRU_Users[VRU_User_ID].Camera_Certainty > 30) {
            VRU_Users[VRU_User_ID].Camera_Data = "Caution " + VRU_Users[VRU_User_ID].Camera_Certainty + "%";
        } else 
        {
            VRU_Users[VRU_User_ID].Camera_Data = "Safe 0%";
        }

        res.json({User_Name: VRU_Users[VRU_User_ID].Name, Status: "Data Updated"});
    }
    else
    {
        res.send({User_Name: "Unknown", Status: "ERROR"});
    }
});

router.post('/Update_UltraSonic/:ID', (req, res) => {
    const VRU_User_ID = parseInt(req.params.ID);
    if(VRU_User_ID >= 0 & VRU_User_ID <= 2) {
        
        const Range = parseFloat(req.body.UltraSonic_Range);
        if(Range < 30)
        {
            VRU_Users[VRU_User_ID].UltraSonic_Data = "Imminent Collision";
        }
        else if(Range < 60)
        {
            VRU_Users[VRU_User_ID].UltraSonic_Data = "Possible Collision";
        } 
        else 
        {
            VRU_Users[VRU_User_ID].UltraSonic_Data = "No Collision";
        }
        VRU_Users[VRU_User_ID].UltraSonic_Range = Range;        
        
        const Essentials_Data = {
			Car_Movement: VRU_Users[VRU_User_ID].Car_Movement,
			Auto_Stop: VRU_Users[VRU_User_ID].Auto_Stop
		}
		res.json(Essentials_Data);
    }
    else
    {
        res.send({User_Name: "Unknown", Status: "ERROR"});
    }
});

function GPS_Distance(lat1,
    lat2, lon1, lon2)
{

// The math module contains a function
// named toRadians which converts from
// degrees to radians.
lon1 =  lon1 * Math.PI / 180;
lon2 = lon2 * Math.PI / 180;
lat1 = lat1 * Math.PI / 180;
lat2 = lat2 * Math.PI / 180;

// Haversine formula
let dlon = lon2 - lon1;
let dlat = lat2 - lat1;
let a = Math.pow(Math.sin(dlat / 2), 2)
+ Math.cos(lat1) * Math.cos(lat2)
* Math.pow(Math.sin(dlon / 2),2);

let c = 2 * Math.asin(Math.sqrt(a));

// Radius of earth in kilometers. Use 3956
// for miles
let r = 6371000;

// calculate the result
return(c * r);
}

function Update_GPS_Minimum_Distance() {
    //User 0
    let Distance_0_1 = Math.round(Math.abs(GPS_Distance(VRU_Users[0].Latitude, VRU_Users[1].Latitude, VRU_Users[0].Longitude, VRU_Users[1].Longitude)));
    let Distance_0_2 = Math.round(Math.abs(GPS_Distance(VRU_Users[0].Latitude, VRU_Users[2].Latitude, VRU_Users[0].Longitude, VRU_Users[2].Longitude)));
    if (Distance_0_1 < Distance_0_2) {
        VRU_Users[0].GPS_Range = Distance_0_1;
        if (Distance_0_1 < 50 && VRU_Users[1].Speed > 0) {
            VRU_Users[0].GPS_Data = "Warning"
        } else if (Distance_0_1 < 50) {
            VRU_Users[0].GPS_Data = "Stay Alert";
        } else if (Distance_0_1 < 100 && VRU_Users[1].Speed > 0) {
            VRU_Users[0].GPS_Data = "Caution"
        } else {
            VRU_Users[0].GPS_Data = "Safe"
        }
    } else {
        VRU_Users[0].GPS_Range = Distance_0_2;
        if (Distance_0_2 < 50 && VRU_Users[2].Speed > 0) {
            VRU_Users[0].GPS_Data = "Warning"
        } else if (Distance_0_2 < 50) {
            VRU_Users[0].GPS_Data = "Stay Alert";
        } else if (Distance_0_2 < 100 && VRU_Users[2].Speed > 0) {
            VRU_Users[0].GPS_Data = "Caution"
        } else {
            VRU_Users[0].GPS_Data = "Safe"
        }
    }

    //User 1
    let Distance_1_0 = Math.round(Math.abs(GPS_Distance(VRU_Users[1].Latitude, VRU_Users[0].Latitude, VRU_Users[1].Longitude, VRU_Users[0].Longitude)));
    let Distance_1_2 = Math.round(Math.abs(GPS_Distance(VRU_Users[1].Latitude, VRU_Users[2].Latitude, VRU_Users[1].Longitude, VRU_Users[2].Longitude)));
    if (Distance_1_0 < Distance_1_2) {
        VRU_Users[1].GPS_Range = Distance_1_0;
        if (Distance_1_0 < 50 && VRU_Users[0].Speed > 0) {
            VRU_Users[1].GPS_Data = "Warning";
        } else if (Distance_1_0 < 50) {
            VRU_Users[1].GPS_Data = "Stay Alert";
        } else if (Distance_1_0 < 100 && VRU_Users[0].Speed > 0) {
            VRU_Users[1].GPS_Data = "Caution";
        } else {
            VRU_Users[1].GPS_Data = "Safe";
        }
    } else {
        VRU_Users[1].GPS_Range = Distance_1_2;
        if (Distance_1_2 < 50 && VRU_Users[2].Speed > 0) {
            VRU_Users[1].GPS_Data = "Warning";
        } else if (Distance_1_2 < 50) {
            VRU_Users[1].GPS_Data = "Stay Alert";
        } else if (Distance_1_2 < 100 && VRU_Users[2].Speed > 0) {
            VRU_Users[1].GPS_Data = "Caution";
        } else {
            VRU_Users[1].GPS_Data = "Safe";
        }
    }

    //User 2
    let Distance_2_0 = Math.round(Math.abs(GPS_Distance(VRU_Users[2].Latitude, VRU_Users[0].Latitude, VRU_Users[2].Longitude, VRU_Users[0].Longitude)));
    let Distance_2_1 = Math.round(Math.abs(GPS_Distance(VRU_Users[2].Latitude, VRU_Users[1].Latitude, VRU_Users[2].Longitude, VRU_Users[1].Longitude)));
    if (Distance_2_0 < Distance_2_1) {
        VRU_Users[2].GPS_Range = Distance_2_0;
        if (Distance_2_0 < 50 && VRU_Users[0].Speed > 0) {
            VRU_Users[2].GPS_Data = "Warning"
        } else if (Distance_2_0 < 50) {
            VRU_Users[2].GPS_Data = "Stay Alert";
        } else if (Distance_2_0 < 100 && VRU_Users[0].Speed > 0) {
            VRU_Users[2].GPS_Data = "Caution"
        } else {
            VRU_Users[2].GPS_Data = "Safe"
        }
    } else {
        VRU_Users[2].GPS_Range = Distance_2_1;
        if (Distance_2_1 < 50 && VRU_Users[1].Speed > 0) {
            VRU_Users[2].GPS_Data = "Warning"
        } else if (Distance_2_1 < 50) {
            VRU_Users[2].GPS_Data = "Stay Alert";
        } else if (Distance_2_1 < 100 && VRU_Users[1].Speed > 0) {
            VRU_Users[2].GPS_Data = "Caution"
        } else {
            VRU_Users[2].GPS_Data = "Safe"
        }
    }
}

module.exports = router;