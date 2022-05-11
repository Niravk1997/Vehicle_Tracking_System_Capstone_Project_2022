import React from 'react';
import Dashboard_Menu from './Dashboard_Menu';
import User_Info_Panel from './User_Info_Panel';
import { Container, Row, Col, Card} from "react-bootstrap";
import Users_Location_Map from './Users_Location_Map';
import "./Dashboard.css";

function Dashboard() {
    const Address = "http://192.168.0.57:3443/VRU_User_GET/";
    return (
        <div>
            <Dashboard_Menu/>
            <Card className="Users_map">
                <Users_Location_Map Address={Address}/>
            </Card>
            <Container>
                <Row>
                    <Col>
                        <User_Info_Panel Address={Address} ID="0"/>
                    </Col>
                    <Col>
                        <User_Info_Panel Address={Address} ID="1"/>
                    </Col>
                    <Col>
                        <User_Info_Panel Address={Address} ID="2"/>
                    </Col>
                </Row>
            </Container>
        </div>
    );
}

export default Dashboard;