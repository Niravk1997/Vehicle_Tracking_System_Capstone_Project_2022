const express = require('express');
const logger = require('./scripts/logger');

const https = require('https');
const path = require('path');
const fs = require('fs');

const app = express();
const port = process.env.PORT || 5000;

var cors = require('cors');
const { dirname } = require('path');
app.use(cors());

//app.use(logger);

//Body Parser
app.use(express.json());
app.use(express.urlencoded({extended: false}));

app.use('/VRU_User_GET', require('./scripts/VRU_Users_GET'));
app.use('/VRU_User_POST', require('./scripts/VRU_Users_POST'));

// HTTP Connection
app.listen(3443, () => console.log(`Listening on port 3443`));

// HTTPS Connection
const sslserver = https.createServer({
	key: fs.readFileSync(path.join(__dirname, 'certificates', 'key.pem')),
	cert: fs.readFileSync(path.join(__dirname, 'certificates', 'cert.pem'))
}, app);

sslserver.listen(port, () => console.log(`Listening on port ${port}`))