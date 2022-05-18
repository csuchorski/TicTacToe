namespace TicTacToe.Models
{
    public class GameController
    {
        private int[,] board = new int[3, 3];
        public int[,] Board
        {
            get { return board; }
            set { board = value; }
        }

        public bool IsMoveValid(byte x, byte y)
        {
            if (Board[x, y] != 0)
            {
                return false;
            }
            return true;
        }

        public void MakeMove(byte x, byte y, byte piece)
        {
            Board[x, y] = piece;
        }
        public bool IsMoveWinning()
        {
            if (Board[0,0] + Board[0,1] + Board[0,2] == 3) return true;

            for (int i = 0; i < 3; i++)
            {
                int sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sum+=Board[i,j];
                }
                if (sum == 3 || sum == 6) return true;
                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sum += Board[j, i];
                }
                if (sum == 3 || sum == 6) return true;
            }
            if (Board[0, 0] + Board[1, 1] + Board[2, 2] == 3 || Board[0, 0] + Board[1, 1] + Board[2, 2] == 6) return true;
            if (Board[2, 0] + Board[1, 1] + Board[0, 2] == 3 || Board[2, 0] + Board[1, 1] + Board[0, 2] == 6) return true;

            return false;
        }
    }

    public enum SquareState
    {
        empty,
        cross,
        circle
    }
}
