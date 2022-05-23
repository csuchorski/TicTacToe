using Microsoft.AspNetCore.SignalR;

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
            if(LobbyAssignmentDict.ContainsKey(Context.ConnectionId))
            {
                await LeaveLobby(Context.ConnectionId);
            }         
            return base.OnDisconnectedAsync(exception);
        }


        public async Task LeaveLobby(string connID)
        {
            int LobbyId = LobbyAssignmentDict[connID];
            await Groups.RemoveFromGroupAsync(connID, Convert.ToString(LobbyId));
            LobbyCapacityDict[LobbyId]--;
            if (LobbyCapacityDict[LobbyId] == 0)
            {
                AvailableLobbies.Remove(LobbyId);
            }
            LobbyAssignmentDict.Remove(connID);
        }

        public async Task JoinLobby(int LobbyId)
        {
            
        }

        public async void JoinRandomLobby()
        {
            int? LobbyId = FindLobby();
            if(LobbyId == null)
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
    }
}
