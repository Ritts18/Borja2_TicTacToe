namespace TicTacToe.Models
{
    public class GameModel
    {
        public char[,] Board { get; set; }
        public char CurrentPlayer { get; set; }
        public string Message { get; set; }
        public int PlayerXWins { get; set; }
        public int PlayerOWins { get; set; }
        public int Draws { get; set; }
        public bool IsAgainstAI { get; set; } = false;
        public List<(int, int)> WinningCells { get; set; } = new List<(int, int)>();

        public GameModel()
        {
            Board = new char[3, 3];
            ResetBoard();
        }

        public void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Board[i, j] = ' ';
            CurrentPlayer = 'X';
            Message = "Player X's turn";
            WinningCells.Clear();
        }

        public void RestartGame()
        {
            PlayerXWins = 0;
            PlayerOWins = 0;
            Draws = 0;
            ResetBoard();
        }

        public bool MakeMove(int row, int col)
        {
            if (Board[row, col] == ' ' && WinningCells.Count == 0)
            {
                Board[row, col] = CurrentPlayer;
                if (CheckWin(CurrentPlayer))
                {
                    if (CurrentPlayer == 'X') PlayerXWins++;
                    else PlayerOWins++;

                    Message = $"Player {CurrentPlayer} wins!";
                    return true;
                }
                if (IsBoardFull())
                {
                    Draws++;
                    Message = "It's a draw!";
                    return true;
                }
                CurrentPlayer = (CurrentPlayer == 'X') ? 'O' : 'X';
                Message = $"Player {CurrentPlayer}'s turn";

                if (IsAgainstAI && CurrentPlayer == 'O')
                {
                    MakeAIMove();
                }

                return true;
            }
            return false;
        }

        private void MakeAIMove()
        {
            (int bestRow, int bestCol) = GetBestMove();
            MakeMove(bestRow, bestCol);
        }

        private (int, int) GetBestMove()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (Board[i, j] == ' ')
                        return (i, j);
            return (0, 0);
        }

        private bool CheckWin(char player)
        {
            WinningCells.Clear();
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] == player && Board[i, 1] == player && Board[i, 2] == player)
                {
                    WinningCells.Add((i, 0)); WinningCells.Add((i, 1)); WinningCells.Add((i, 2));
                    return true;
                }
                if (Board[0, i] == player && Board[1, i] == player && Board[2, i] == player)
                {
                    WinningCells.Add((0, i)); WinningCells.Add((1, i)); WinningCells.Add((2, i));
                    return true;
                }
            }
            if (Board[0, 0] == player && Board[1, 1] == player && Board[2, 2] == player)
            {
                WinningCells.Add((0, 0)); WinningCells.Add((1, 1)); WinningCells.Add((2, 2));
                return true;
            }
            if (Board[0, 2] == player && Board[1, 1] == player && Board[2, 0] == player)
            {
                WinningCells.Add((0, 2)); WinningCells.Add((1, 1)); WinningCells.Add((2, 0));
                return true;
            }
            return false;
        }

        private bool IsBoardFull()
        {
            foreach (char cell in Board)
                if (cell == ' ')
                    return false;
            return true;
        }
    }
}
