"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/connect").build();

connection.onclose(() => {
    console.log("Disconnected");
})

connection.on("startMatch", startMatch);
connection.on("endMatch", endMatch);

let stopBtn = document.getElementById("endBtn");
const lobbyIdPara = document.getElementById("LobbyIdVal");
const teamValPara = document.getElementById("TeamVal");
const boardArray = [[0,0,0],[0,0,0],[0,0,0]]; //0=>empty, 1=>circle, 2=>cross
const squareHTMLCollection = document.getElementsByClassName("square");
const squareArray = [...squareHTMLCollection];

endBtn.addEventListener("click", endMatch);

squareArray.forEach(square => {
    square.addEventListener('click', () => {
        let row = square.id.charAt(7);
        let col = square.id.charAt(9);
        if (boardArray[row][col] == 0) {
            placeMove(col, row, teamValPara.textContent);
        }
        else {
            alert("Square taken");
        }
    })
})

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
    let verdict = await connection.invoke("TryMakeMove", x, y, piece, lobbyIdPara.textContent);
    if (!verdict) {
        alert("Move invalid");
    }
}

async function showDraw() {

}

async function showGameover(piece) {
    alert("Game is over")
}

start();
