"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/connect").build();

connection.onclose(() => {
    console.log("Disconnected");
})

connection.on("startMatch", startMatch);
connection.on("endMatch", endMatch);

connection.on("placeMove", placeMove);
connection.on("showDraw", showDraw);
connection.on("showGameover", showGameover);

let stopBtn = document.getElementById("endBtn");
const lobbyIdPara = document.getElementById("LobbyIdVal");
const teamValPara = document.getElementById("TeamVal");
const boardArray = [[0,0,0],[0,0,0],[0,0,0]]; //0=>empty, 1=>circle, 2=>cross
const squareHTMLCollection = document.getElementsByClassName("square");
const squareArray = [...squareHTMLCollection];

endBtn.addEventListener("click", endMatch);

squareArray.forEach(square => {
    square.addEventListener('click', async () => {
        let row = Number(square.id.charAt(7));
        let col = Number(square.id.charAt(9));
        if (boardArray[row][col] == 0) {
            await connection.invoke("TryMakeMove", row, col, Number(teamValPara.textContent), Number(lobbyIdPara.textContent));
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

async function placeMove(x,y,piece) {
    boardArray[x][y] = piece;
    let squareToChangeId = `square-${x}-${y}`; 
    console.log(squareToChangeId);
    document.getElementById(squareToChangeId).textContent = piece;
}

function endMatch() {
    connection.invoke("LeaveLobby")
    window.location.replace("/");
    console.log("Match ended")
}

async function showDraw() {
    alert("Game drawn");
    window.location.replace("/");
}

async function showGameover(piece) {
    alert("Game is over");
    window.location.replace("/");
}

start();
