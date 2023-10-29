using GameOfLife.Context;
using GameOfLife.Domain;
using GameOfLife.Services.Interfaces;

namespace GameOfLife.Services
{
    public class GameBoardService : IGameBoardService
    {
        private readonly ILogger<GameBoardService> _logger;
        private readonly BoardContext _boardContext;

        private HashSet<string> _previousStates = new HashSet<string>();

        public GameBoardService(ILogger<GameBoardService> logger, BoardContext boardContext)
        {
            _logger = logger;
            _boardContext = boardContext;
        }

        public async Task<int> CreateGameBoardAsync(int[][] board)
        {
            var gameBoard = new GameBoard
            {
                Board = board,
            };

            _boardContext.GameBoards.Add(gameBoard);
            await _boardContext.SaveChangesAsync();

            return gameBoard.BoardId;
        }


        public async Task<GameBoard> GetGameBoardAsync(int boardId)
        {
            var gameBoard = await _boardContext.GameBoards.FindAsync(boardId);

            return gameBoard;
        }

        public async Task<GameBoard> GetGameBoardNextStateAsync(int boardId, int numberOfGenerations)
        {
            var gameBoard = await _boardContext.GameBoards.FindAsync(boardId);

            if (gameBoard == null)
            {
                return null;
            }

            for (int i = 0; i < numberOfGenerations || i == 0; i++)
            {
                var nextGenerationBoard = CalculateNextGeneration(gameBoard.Board);
                gameBoard.Board = nextGenerationBoard;
            }

            _boardContext.GameBoards.Update(gameBoard);
            await _boardContext.SaveChangesAsync();

            return gameBoard;
        }

        public async Task<GameBoard> GetGameBoardFinalState(int boardId, int numberOfGenerations)
        {
            var gameBoard = await _boardContext.GameBoards.FindAsync(boardId);

            if (gameBoard == null)
            {
                return null;
            }

            for (int i = 0; i < numberOfGenerations; i++)
            {
                var nextGenerationBoard = CalculateNextGeneration(gameBoard.Board);
                gameBoard.Board = nextGenerationBoard;

                if (_previousStates.Contains(gameBoard.BoardJson))
                {
                    _boardContext.GameBoards.Update(gameBoard);
                    await _boardContext.SaveChangesAsync();

                    return gameBoard;
                }

                _previousStates.Add(gameBoard.BoardJson);
            }

            throw new InvalidOperationException("Game board did not reach conlusion within the specific number of generations.");
        }

        private int[][] CalculateNextGeneration(int[][] currentGeneration)
        {
            var rows = currentGeneration.Length;
            var columns = currentGeneration[0].Length;
            var nextGeneration = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                nextGeneration[i] = new int[columns];
                for (int j = 0; j < columns; j++)
                {
                    var liveNeighbors = CountLiveNeighbors(currentGeneration, i, j);

                    if (currentGeneration[i][j] == 1)
                    {
                        nextGeneration[i][j] = (liveNeighbors == 2 || liveNeighbors == 3) ? 1 : 0;
                    }
                    else
                    {
                        nextGeneration[i][j] = (liveNeighbors == 3) ? 1 : 0;
                    }
                }
            }

            return nextGeneration;
        }

        private static int CountLiveNeighbors(int[][] board, int row, int column)
        {
            var count = 0;
            var rows = board.Length;
            var columns = board[0].Length;

            int[] rowOffsets = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] columnOffsets = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int newRow = row + rowOffsets[i];
                int newCol = column + columnOffsets[i];
                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < columns && board[newRow][newCol] == 1)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
