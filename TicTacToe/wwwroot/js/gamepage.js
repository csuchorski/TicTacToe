"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/connect").build();

connection.onclose(() => {
    console.log("Disconnected");
})


connection.on("startMatch", startMatch);
connection.on("endMatch", endMatch);

async function start() {
    try {
        await connection.start();
        console.log("Connected");
        lobbyIdPara.textContent = await connection.invoke("GetLobbyId");
    }
    catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

let stopBtn = document.getElementById("endBtn");
const lobbyIdPara = document.getElementById("LobbyIdVal");
const teamValPara = document.getElementById("TeamVal");

endBtn.addEventListener("click", endMatch);

function startMatch(team) {
    console.log("Match started")
    teamValPara.textContent = team;
    board.style.display = "block";
}

function endMatch() {
    connection.invoke("LeaveLobby")
    window.location.replace("https://localhost:7016");
    console.log("Match ended")
}

async function placeMove(x , y, piece) {

}

async function showDraw() {

}

async function showGameover(piece) {
    alert("Game is over")
}



start();
