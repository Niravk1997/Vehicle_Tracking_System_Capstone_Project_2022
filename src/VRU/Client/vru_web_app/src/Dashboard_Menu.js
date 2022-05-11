import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Navbar } from "react-bootstrap";

class Dashboard_Menu extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      curTime: new Date().toLocaleString(),
    };
  }

  tick() {
    this.setState({ curTime: new Date().toLocaleString() });
  }

  componentDidMount() {
    this.tick();
    this.interval = setInterval(() => this.tick(), 1000); //update every 1 second
  }

  componentWillUnmount() {
    clearInterval(this.interval);
  }

  render() {
    return (
      <div className="menu_navbar">
        <Navbar bg="dark" variant="dark">
          <Navbar.Brand href="#home">
            <img
              alt=""
              src={`./logo192.png`}
              width="30"
              height="30"
              className="d-inline-block align-top"
            />{" "}
            VRU Dashboard
          </Navbar.Brand>
          <Navbar.Brand className="ml-auto">{this.state.curTime}</Navbar.Brand>
        </Navbar>
      </div>
    );
  }
}

export default Dashboard_Menu;