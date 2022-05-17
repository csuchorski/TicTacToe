"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/connect").build();

async function start() {
    try {
        await connection.start();
        console.log("Connected");
    }
    catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

connection.onclose(() => {
    console.log("Disconnected");
})

start();