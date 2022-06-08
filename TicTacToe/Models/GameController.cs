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

        private bool turnController = true;

        public bool TurnController
        {
            get { return turnController; }
            set { turnController = value; }
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
            foreach (var square in Board)
            {
                if (square != 0) count++;
            }
            if(count == 9) return true;
            return false;
        }
        public bool IsMoveWinning()
        {
            if (CheckForHorizontalWin()) return true;
            if (CheckForVerticalWin()) return true;
            if (CheckForDiagonalWin()) return true;

            return false;
        }

        public bool CheckForVerticalWin()
        {
            int counter;
            for (int i = 0; i < 3; i++)
            {
                counter = 0;
                int checkForVal = Board[i, 0];
                if (checkForVal == 0) continue;
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] == checkForVal) 
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }
                if(counter == 3) return true;
            }
            return false;
        }
        public bool CheckForHorizontalWin()
        {
            int counter;
            for (int i = 0; i < 3; i++)
            {
                counter = 0;
                int checkForVal = Board[0, i];
                if (checkForVal == 0) continue;
                for (int j = 0; j < 3; j++)
                {
                    if (Board[j, i] == checkForVal)
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (counter == 3) return true;
            }
            return false;
        }

        public bool CheckForDiagonalWin()
        {
            int counterLeftToRight = 0;
            int counterRightToLeft = 0;
            int checkForVal = Board[1, 1];
            if (checkForVal == 0) return false; //if middle is empty there's no point in checking further
            for (int i = 0; i < 3; i++)
            {
                if (Board[i,i] == checkForVal) counterLeftToRight++;
            }
            if(counterLeftToRight == 3) return true;

            int j = 0;
            for (int i = 2; i >= 0; i--)
            {
                if (Board[i, j] == checkForVal) counterRightToLeft++;
                j++;
            }
            if (counterRightToLeft == 3) return true;


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
