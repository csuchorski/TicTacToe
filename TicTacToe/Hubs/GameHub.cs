using Microsoft.AspNetCore.SignalR;
using TicTacToe.Models;

namespace TicTacToe.Hubs
{
    public class GameHub : Hub
    {
        public static Dictionary<string, int> LobbyAssignmentDict = new();
        public static Dictionary<int, byte> LobbyCapacityDict = new();
        public static List<int> AvailableLobbies = new();

        Random rand = new();

        public async override Task<Task> OnDisconnectedAsync(Exception? exception)
        {
            if (LobbyAssignmentDict.ContainsKey(Context.ConnectionId))
            {
                await LeaveLobby(Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }
        public async override Task<Task> OnConnectedAsync()
        {
            await JoinRandomLobby();
            return base.OnConnectedAsync();
        }

        //Lobby handling

        public async Task LeaveLobby(string connID)
        {
            int LobbyId = LobbyAssignmentDict[connID];
            await Clients.Group(Convert.ToString(LobbyId)).SendAsync("endMatch");

            //If one player leaves the whole lobby gets disbanded and match is over
            AvailableLobbies.Remove(LobbyId);
            LobbyAssignmentDict.Remove(connID);

            string secondUser = LobbyAssignmentDict.First(val => val.Value == LobbyId).Key;
            if (!string.IsNullOrEmpty(secondUser))
            {
                LobbyAssignmentDict.Remove(secondUser);
                await Groups.RemoveFromGroupAsync(secondUser, Convert.ToString(LobbyId));
            }
            await Groups.RemoveFromGroupAsync(connID, Convert.ToString(LobbyId));
            LobbyCapacityDict.Remove(LobbyId);
            GameStorage.gameStorage.Remove(LobbyId);
        }

        public async Task JoinLobby(int LobbyId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Convert.ToString(LobbyId));
            LobbyAssignmentDict.Add(Context.ConnectionId, LobbyId);
            LobbyCapacityDict[LobbyId]++;
            if (LobbyCapacityDict[LobbyId] == 2)
            {
                GameStorage.gameStorage.Add(LobbyId, new GameController());
                var players = LobbyAssignmentDict.Where(a => a.Value == LobbyId).Select(k => k.Key);
                 
                AvailableLobbies.Remove(LobbyId);
                await Clients.Client(players.ElementAt(0)).SendAsync("startMatch", 2);
                await Clients.Client(players.ElementAt(1)).SendAsync("startMatch", 1);
                await Clients.Client(players.ElementAt(1)).SendAsync("allowMove");
            }
        }

        public async Task JoinRandomLobby()
        {
            int? LobbyId = FindLobby();
            if (LobbyId == null)
            {
                LobbyId = CreateLobby();
            }
            await JoinLobby(LobbyId.Value);
        }

        public int? FindLobby()
        {
            if (AvailableLobbies.Count == 0) return null;
            else
            {
                return AvailableLobbies.First();
            }
        }

        public int CreateLobby()
        {
            int LobbyId;
            do
            {
                LobbyId = rand.Next(0, int.MaxValue);
            } while (LobbyCapacityDict.ContainsKey(LobbyId));

            LobbyCapacityDict.Add(LobbyId, 0);
            AvailableLobbies.Add(LobbyId);

            return LobbyId;
        }

        public int GetLobbyId()
        {
            return LobbyAssignmentDict[Context.ConnectionId];
        }

        //Move handling

        public bool TryMakeMove(byte x, byte y, byte piece, int LobbyID)
        {
            if (!GameStorage.gameStorage[LobbyID].IsMoveValid(x, y)) return false;
            GameStorage.gameStorage[LobbyID].MakeMove(x, y, piece);
            if(GameStorage.gameStorage[LobbyID].IsGameOver() == "continue")
            {
                Clients.Group(Convert.ToString(LobbyID)).SendAsync("placeMove", x, y, piece);
            }
            else if (GameStorage.gameStorage[LobbyID].IsGameOver() == "draw")
            {
                Clients.Group(Convert.ToString(LobbyID)).SendAsync("showDraw");
            }
            else
            {
                Clients.Group(Convert.ToString(LobbyID)).SendAsync("showGameover", piece);
            }
            return true;
        }
    }
}
