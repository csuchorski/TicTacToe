"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/connect").build();

connection.onclose(() => {
    console.log("Disconnected");
})

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

async function placeMove(x , y, piece) {

}

async function showDraw() {

}

async function showGameover(piece) {
    alert("Game is over")
}



start();