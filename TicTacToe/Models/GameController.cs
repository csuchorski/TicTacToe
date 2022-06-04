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

        public string IsGameOver()
        {
            if(IsMoveWinning() == true) return "gameover";
            if (IsGameDrawn() == true) return "draw";
            return "continue";
        }

        public bool IsGameDrawn()
        {
            byte count = 0;
            Func<int, int, bool> draw = (x, y) => Board[x, y] != 0;
            foreach (var square in Board)
            {
                if (square != 0) count++;
            }
            if(count == 9) return true;
            return false;
        }
        public bool IsMoveWinning()
        {
            if (CheckForHorizontalWin() == true) return true;
            if (CheckForVerticalWin() == true) return true;
            //if (Board[0,0] + Board[0,1] + Board[0,2] == 3) return true;
            //for (int i = 0; i < 3; i++)
            //{
            //    int sum = 0;
            //    for (int j = 0; j < 3; j++)
            //    {
            //        sum+=Board[i,j];
            //    }
            //    if (sum == 3 || sum == 6) return true;
            //    sum = 0;
            //    for (int j = 0; j < 3; j++)
            //    {
            //        sum += Board[j, i];
            //    }
            //    if (sum == 3 || sum == 6) return true;
            //}
            //if (Board[0, 0] + Board[1, 1] + Board[2, 2] == 3 || Board[0, 0] + Board[1, 1] + Board[2, 2] == 6) return true;
            //if (Board[2, 0] + Board[1, 1] + Board[0, 2] == 3 || Board[2, 0] + Board[1, 1] + Board[0, 2] == 6) return true;

            return false;
        }

        public bool CheckForVerticalWin()
        {
            for (int i = 0; i < 3; i++)
            {
                int checkForVal = Board[i, 0];
                if (checkForVal == 0) return false;
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] != checkForVal) return false;
                }
            }
            return true;
        }
        public bool CheckForHorizontalWin()
        {
            for (int i = 0; i < 3; i++)
            {
                int checkForVal = Board[0, i];
                if(checkForVal == 0) return false;

                for (int j = 0; j < 3; j++)
                {
                    if(Board[j,i] != checkForVal) return false;
                }
            }
            return true;
        }
    }


    public enum SquareState
    {
        empty,
        cross,
        circle
    }
}
