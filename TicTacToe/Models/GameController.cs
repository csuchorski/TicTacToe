namespace TicTacToe.Models
{
    public class GameController
    {
        private int [,] board = new int[3,3];
        public int [,] Board
        {
            get { return board; }
            set { board = value; }
        } 

        public bool IsMoveValid(byte x, byte y)
        {
            return true;
        }

        public void MakeMove(byte x, byte y, bool piece)
        {

        }
        public bool IsMoveWinning()
        {

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
