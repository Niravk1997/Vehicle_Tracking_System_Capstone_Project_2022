import React from "react";
import { TileLayer, MapContainer, Marker, Popup, Tooltip } from "react-leaflet";

class Users_Location_Map extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
          Web_Server_URL: props.Address,

          city: "Toronto",
          zoom: 50,

          Default_Longitude: -79.37723594658459, 
          Default_Latitude: 43.657693324437055,

          ID_0: 0,
          User_0_Name: '',
          User_0_Longitude: 0,
          User_0_Latitude: 0,
          User_0_Distance: 0,

          ID_1: 1,
          User_1_Name: '',
          User_1_Longitude: 0,
          User_1_Latitude: 0,
          User_1_Distance: 0,

          ID_2: 2,
          User_2_Name: '',
          User_2_Longitude: 0,
          User_2_Latitude: 0,
          User_2_Distance: 0
        };
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        this.setState((prevState) => ({
          switch: !prevState.switch,
        }));
      }
    
      tick() {
        fetch(this.state.Web_Server_URL + this.state.ID_0)
          .then((res) => res.json())
          .then((data) => {
            if (data.cod === "404") {
              this.setState({
                User_0_Name: '',
                User_0_Latitude: 0,
                User_0_Longitude: 0,
                User_0_Distance: 0,
              });
            } else {
              this.setState({
                User_0_Name: data.Name,
                  User_0_Latitude: data.Latitude,
                  User_0_Longitude: data.Longitude,
                  User_0_Distance: data.GPS_Range,
              });
            }
          });

          fetch(this.state.Web_Server_URL + this.state.ID_1)
          .then((res) => res.json())
          .then((data) => {
            if (data.cod === "404") {
              this.setState({
                User_1_Name: '',
                User_1_Latitude: 0,
                User_1_Longitude: 0,
                User_1_Distance: 0,
              });
            } else {
              this.setState({
                User_1_Name: data.Name,
                  User_1_Latitude: data.Latitude,
                  User_1_Longitude: data.Longitude,
                  User_1_Distance: data.GPS_Range,
              });
            }
          });

          fetch(this.state.Web_Server_URL + this.state.ID_2)
          .then((res) => res.json())
          .then((data) => {
            if (data.cod === "404") {
              this.setState({
                User_2_Name: '',
                User_2_Latitude: 0,
                User_2_Longitude: 0,
                User_2_Distance: 0,
              });
            } else {
              this.setState({
                User_2_Name: data.Name,
                  User_2_Latitude: data.Latitude,
                  User_2_Longitude: data.Longitude,
                  User_2_Distance: data.GPS_Range,
              });
            }
          });
      }
    
      componentDidMount() {
        this.tick();
        this.interval = setInterval(() => this.tick(), 1000); //update every 200 ms
      }
    
      componentWillUnmount() {
        clearInterval(this.interval);
      }
  
    render() {
      return (
        <MapContainer
        center={[this.state.Default_Latitude, this.state.Default_Longitude]}
        zoom={this.state.zoom}
        scrollWheelZoom={true}
      >
        <TileLayer
          attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        <Marker position={[this.state.User_0_Latitude, this.state.User_0_Longitude]}>
          <Popup>Minimum Distance: {this.state.User_0_Distance}</Popup>
          <Tooltip
            direction="bottom"
            offset={[-15, 30]}
            opacity={1}
            permanent="true"
          >
            {this.state.User_0_Name}
          </Tooltip>
        </Marker>

        <Marker position={[this.state.User_1_Latitude, this.state.User_1_Longitude]}>
          <Popup>Minimum Distance: {this.state.User_1_Distance}</Popup>
          <Tooltip
            direction="bottom"
            offset={[-15, 30]}
            opacity={1}
            permanent="true"
          >
            {this.state.User_1_Name}
          </Tooltip>
        </Marker>

        <Marker position={[this.state.User_2_Latitude, this.state.User_2_Longitude]}>
          <Popup>Minimum Distance: {this.state.User_2_Distance}</Popup>
          <Tooltip
            direction="bottom"
            offset={[-15, 30]}
            opacity={1}
            permanent="true"
          >
            {this.state.User_2_Name}
          </Tooltip>
        </Marker>
      </MapContainer>
      );
    }
  }
  
  export default Users_Location_Map;