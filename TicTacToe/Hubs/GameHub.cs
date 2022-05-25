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
        GameController game = new();

        public async override Task<Task> OnDisconnectedAsync(Exception? exception)
        {
            if (LobbyAssignmentDict.ContainsKey(Context.ConnectionId))
            {
                await LeaveLobby(Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }

        //Lobby handling

        public async Task LeaveLobby(string connID)
        {
            int LobbyId = LobbyAssignmentDict[connID];
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
        }

        public async Task JoinLobby(int LobbyId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Convert.ToString(LobbyId));
            LobbyAssignmentDict.Add(Context.ConnectionId, LobbyId);
            LobbyCapacityDict[LobbyId]++;
            if (LobbyCapacityDict[LobbyId] == 2)
            {
                var players = LobbyAssignmentDict.Where(a => a.Value == LobbyId).Select(k => k.Key);
                 
                AvailableLobbies.Remove(LobbyId);
                await Clients.User(players.ElementAt(0)).SendAsync("startMatch", "first");
                await Clients.User(players.ElementAt(1)).SendAsync("startMatch", "second");
            }
        }

        public async void JoinRandomLobby()
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

        //Move handling

        public bool TryMakeMove(byte x, byte y, byte piece, int LobbyID)
        {
            if (!game.IsMoveValid(x, y)) return false;
            game.MakeMove(x, y, piece);
            if(game.IsGameOver() == "continue")
            {
                Clients.Group(Convert.ToString(LobbyID)).SendAsync("placeMove", x, y, piece);
            }
            if (game.IsGameOver() == "draw")
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
