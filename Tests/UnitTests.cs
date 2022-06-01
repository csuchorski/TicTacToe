using TicTacToe.Models;

namespace Tests
{
    public class UnitTests
    {
        [Fact]
        public void GameWonVerticalPositive()
        {
            GameController gameController = new GameController();
            gameController.Board[0, 0] = 1;
            gameController.Board[0, 1] = 1;
            gameController.Board[0, 2] = 1;

            Assert.True(gameController.IsMoveWinning() == true);
        }
        [Fact]
        public void GameWonVerticalDiagonalPositive()
        {
            GameController gameController = new GameController();
            gameController.Board[0, 0] = 1;
            gameController.Board[0, 1] = 0;
            gameController.Board[0, 2] = 0;

            gameController.Board[1, 0] = 0;
            gameController.Board[1, 1] = 1;
            gameController.Board[1, 2] = 0;

            gameController.Board[2, 0] = 0;
            gameController.Board[2, 1] = 0;
            gameController.Board[2, 2] = 1;

            Assert.True(gameController.IsGameOver() == "gameover");
        }
        [Fact]
        public void GamePendingPositive()
        {
            GameController gameController = new GameController();
            gameController.Board[0, 0] = 1;
            gameController.Board[0, 1] = 2;
            gameController.Board[0, 2] = 1;

            Assert.True(gameController.IsMoveWinning() == false);
        }

        [Fact]
        public void IsGameOverDrawnPositive()
        {
            GameController gameController = new GameController();
            gameController.Board[0, 0] = 1;
            gameController.Board[0, 1] = 2;
            gameController.Board[0, 2] = 2;

            gameController.Board[1, 0] = 2;
            gameController.Board[1, 1] = 2;
            gameController.Board[1, 2] = 1;

            gameController.Board[2, 0] = 1;
            gameController.Board[2, 1] = 1;
            gameController.Board[2, 2] = 2;

            Assert.True(gameController.IsGameOver() == "draw");
        }
        [Fact]
        public void IsGameOverDrawnFailed()
        {
            GameController gameController = new GameController();
            gameController.Board[0, 0] = 1;
            gameController.Board[0, 1] = 2;
            gameController.Board[0, 2] = 2;

            gameController.Board[1, 0] = 2;
            gameController.Board[1, 1] = 2;
            gameController.Board[1, 2] = 1;

            gameController.Board[2, 0] = 1;
            gameController.Board[2, 1] = 1;
            gameController.Board[2, 2] = 1;

            Assert.False(gameController.IsGameOver() == "draw");
        }
        [Fact]
        public void IsGameOverContinueTrue()
        {
            GameController gameController = new GameController();
            gameController.Board[1, 1] = 1;


            Assert.True(gameController.IsGameOver() == "continue");
        }

    }
}