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
            gameController.Board[0, 1] = 1;
            gameController.Board[1, 1] = 2;
            gameController.Board[2, 1] = 2;

            Assert.True(gameController.IsGameOver() == "continue");
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
        public void IsGameOverGameoverTrue()
        {
            GameController gameController = new GameController();
            gameController.Board[0, 0] = 1;
            gameController.Board[0, 1] = 1;
            gameController.Board[0, 2] = 1;

            Assert.True(gameController.IsGameOver() == "gameover");
        }
        [Fact]
        public void GameOverHorizontalTrue()
        {
            GameController gameController = new();
            gameController.Board[0, 0] = 1;
            gameController.Board[0, 1] = 1;
            gameController.Board[0, 2] = 1;
            gameController.Board[1, 0] = 2;
            gameController.Board[1, 1] = 2;
            gameController.Board[1, 2] = 2;

            Assert.True(gameController.CheckForVerticalWin() == true);
        }

        [Fact]
        public void GameOverVerticalTrue()
        {
            GameController gameController = new();
            gameController.Board[0, 0] = 2;
            gameController.Board[0, 1] = 2;
            gameController.Board[0, 2] = 2;
            gameController.Board[1, 0] = 0;
            gameController.Board[1, 1] = 1;
            gameController.Board[2, 1] = 1;

            Assert.True(gameController.CheckForVerticalWin() == true);
        }

        [Fact]
        public void GameOverDiagonalRtLTrue()
        {
            GameController gameController = new();
            gameController.Board[0, 0] = 1;
            gameController.Board[0, 1] = 1;
            gameController.Board[0, 2] = 2;
            gameController.Board[1, 0] = 0;
            gameController.Board[1, 1] = 2;
            gameController.Board[2, 0] = 2;

            Assert.True(gameController.CheckForDiagonalWin() == true);
        }

        [Fact]
        public void GameOverDiagonalLtRTrue()
        {
            GameController gameController = new();
            gameController.Board[0, 0] = 1;
            gameController.Board[0, 1] = 1;
            gameController.Board[0, 2] = 2;
            gameController.Board[1, 0] = 2;
            gameController.Board[1, 1] = 1;
            gameController.Board[2, 2] = 1;


            Assert.True(gameController.CheckForDiagonalWin() == true);
        }

        [Fact]
        public void GameOverSingleCornerPlaced()
        {
            GameController gameController = new();
            gameController.Board[0, 0] = 1;


            Assert.True(gameController.CheckForDiagonalWin() == false);
        }


    }
}