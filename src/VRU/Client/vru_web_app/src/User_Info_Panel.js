import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Card } from "react-bootstrap";

class User_Info_Panel extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        Web_Server_URL: props.Address,
        ID: props.ID,
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
        Camera_Data: '',
        UltraSonic_Data: '',
        UltraSonic_Range: 0,
        GPS_Data: '',
        GPS_Range: 0,
        Car_Movement: 0,
        Auto_Stop: false,

        Camera_Detect: 'False'
      };
      this.handleChange = this.handleChange.bind(this);
    }
  
    handleChange(event) {
      this.setState((prevState) => ({
        switch: !prevState.switch,
      }));
    }
  
    tick() {
      fetch(this.state.Web_Server_URL + this.state.ID)
        .then((res) => res.json())
        .then((data) => {
          if (data.cod === "404") {
            this.setState({
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
                Camera_Data: '',
                UltraSonic_Data: '',
                UltraSonic_Range: 0,
                GPS_Data: '',
                GPS_Range: 0,
                Car_Movement: 0,
                Auto_Stop: false
            });
          } else {
            this.setState({
                isConnected: data.isConnected,
                Name: data.Name,
                Vehicle: data.Vehicle,
                License_Plate: data.License_Plate,
                Latitude: data.Latitude,
                Longitude: data.Longitude,
                Speed: data.Speed,
                isCollision: data.isCollision,
                Collision_Type: data.Collision_Type,
                Collision_With: data.Collision_With,
                isCamera_Detected: data.isCamera_Detected,
                Camera_Data: data.Camera_Data,
                UltraSonic_Data: data.UltraSonic_Data,
                UltraSonic_Range: data.UltraSonic_Range,
                GPS_Data: data.GPS_Data,
                GPS_Range: data.GPS_Range,
                Car_Movement: data.Car_Movement,
                Auto_Stop: data.Auto_Stop
            });
          }
        });
    }
  
    componentDidMount() {
      this.tick();
      this.interval = setInterval(() => this.tick(), 200); //update every 200 ms
    }
  
    componentWillUnmount() {
      clearInterval(this.interval);
    }
  
    render() {
        let Car_Direction = "";
        if (this.state.isCamera_Detected === false){
            this.setState.Camera_Detect = "False";
        } else 
        {
            this.setState.Camera_Detect = "True";
        }

        if (this.state.Car_Movement === 0) {
            Car_Direction = "Stopped";
        } else if (this.state.Car_Movement === 1) {
            Car_Direction = "Forward";
        } else if (this.state.Car_Movement === 2) {
            Car_Direction = "Backward";
        } else if (this.state.Car_Movement === 3) {
            Car_Direction = "Left";
        } else if (this.state.Car_Movement === 4) {
            Car_Direction = "Right";
        }


        return (
            <Card style={{ width: '20rem' }}>
                <Card.Body>
                    <Card.Title>{this.state.Name}</Card.Title>
                    <Card.Subtitle className="mb-2 text-muted">{this.state.Vehicle} {this.state.License_Plate}</Card.Subtitle>
                    <Card.Text>
                        ID: {this.state.Latitude} <br></br>
                        Latitude: {this.state.Latitude} <br></br>
                        Longitude: {this.state.Longitude} <br></br>
                        Speed: {this.state.Speed} <br></br>
                        GPS Collision: {this.state.GPS_Data} <br></br>
                        GPS Distance (m): {this.state.GPS_Range} <br></br>
                        Ultrasonic Collision:  {this.state.UltraSonic_Data} <br></br>
                        Ultrasonic Range (cm): {this.state.UltraSonic_Range} <br></br>
                        Detected Camera:  {this.state.isCamera_Detected} <br></br>
                        Camera_Data:  {this.state.Camera_Data} <br></br>
                        Car Movement: {Car_Direction}
                    </Card.Text>
                </Card.Body>
            </Card>
        );
    }
  }
  export default User_Info_Panel;