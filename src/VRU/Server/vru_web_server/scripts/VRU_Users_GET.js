const express = require('express');
const VRU_Users = require('./Data');
const router = express.Router();

router.get('/All', (req, res) => {
	res.json(VRU_Users);
});

router.get('/:ID', (req, res) => {
	const User_ID = parseInt(req.params.ID);
	if(User_ID >= 0 & User_ID <= 2)
	{
		res.json(VRU_Users[User_ID]);
	}else
	{
		res.json({VRU_User: "Unknown"});
	}
});

router.get('/Essentials_Car/:ID', (req, res) => {
	const User_ID = parseInt(req.params.ID);
	if(User_ID >= 0 & User_ID <= 2)
	{
		const Essentials_Data = {
			Car_Movement: VRU_Users[User_ID].Car_Movement,
			Auto_Stop: VRU_Users[User_ID].Auto_Stop
		}
		res.json(Essentials_Data);
	}else
	{
		res.json({VRU_User: "Unknown"});
	}
});

router.get('/Essentials_Mobile/:ID', (req, res) => {
	const User_ID = parseInt(req.params.ID);
	if(User_ID >= 0 & User_ID <= 2)
	{
		const Essentials_Data = {
			isCollision: VRU_Users[User_ID].isCollision,
        	Collision_Type: VRU_Users[User_ID].Collision_Type,
        	Collision_With: VRU_Users[User_ID].Collision_With,
        	isCamera_Detected: VRU_Users[User_ID].isCamera_Detected,
        	Camera_Data: VRU_Users[User_ID].Camera_Data,
			Camera_Certainty: VRU_Users[User_ID].Camera_Certainty,
        	UltraSonic_Data: VRU_Users[User_ID].UltraSonic_Data,
        	UltraSonic_Range: VRU_Users[User_ID].UltraSonic_Range,
			GPS_Data: VRU_Users[User_ID].GPS_Data,
			GPS_Range: VRU_Users[User_ID].GPS_Range
		}
		res.json(Essentials_Data);
	}else
	{
		res.json({VRU_User: "Unknown"});
	}
});

router.get('/Essentials_Web/:ID', (req, res) => {
	const User_ID = parseInt(req.params.ID);
	if(User_ID >= 0 & User_ID <= 2)
	{
		const Essentials_Data = {
        	Latitude: VRU_Users[User_ID].Latitude,
        	Longitude: VRU_Users[User_ID].Longitude,
        	Speed: VRU_Users[User_ID].Speed,
        	isCollision: VRU_Users[User_ID].isCollision,
        	Collision_Type: VRU_Users[User_ID].Collision_Type,
        	Collision_With: VRU_Users[User_ID].Collision_With,
        	isCamera_Detected: VRU_Users[User_ID].isCamera_Detected,
        	Camera_Data: VRU_Users[User_ID].Camera_Data,
        	UltraSonic_Data: VRU_Users[User_ID].UltraSonic_Data,
        	UltraSonic_Range: VRU_Users[User_ID].UltraSonic_Range,
			GPS_Data: VRU_Users[User_ID].GPS_Data,
			GPS_Range: VRU_Users[User_ID].GPS_Range,
			Car_Movement: VRU_Users[User_ID].Car_Movement
		}
		res.json(Essentials_Data);
	}else
	{
		res.json({VRU_User: "Unknown"});
	}
});

router.get('/License/:License_Plate', (req, res) => {
	const User_License_Plate = parseInt(req.params.License_Plate);
	const User_Found = VRU_Users.some(VRU_Users => VRU_Users.License_Plate === User_License_Plate)
	if(User_Found)
	{
		res.json(VRU_Users.filter(VRU_Users => VRU_Users.License_Plate === User_License_Plate));
	}else
	{
		res.json({VRU_User: "Unknown"});
	}
});

module.exports = router;