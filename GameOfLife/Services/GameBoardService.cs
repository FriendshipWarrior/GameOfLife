using GameOfLife.Context;
using GameOfLife.Domain;
using GameOfLife.Services.Interfaces;

namespace GameOfLife.Services
{
    public class GameBoardService : IGameBoardService
    {
        private readonly ILogger<GameBoardService> _logger;
        private readonly BoardContext _boardContext;

        public GameBoardService(ILogger<GameBoardService> logger, BoardContext boardContext)
        {
            _logger = logger;
            _boardContext = boardContext;
        }

        public async Task<int> CreateGameBoard(CreateBoardRequest request)
        {
            var gameBoard = new GameBoard
            {
                Board = request.Board,
            };

            _boardContext.GameBoards.Add(gameBoard);
            await _boardContext.SaveChangesAsync();

            return gameBoard.BoardId;
        }

        public async Task<GameBoard> GetBoardNextState(int boardId, int numberOfStates)
        {
            var gameBoard = await _boardContext.GameBoards.FindAsync(boardId);

            if (gameBoard == null)
            {
                return null;
            }

            //TODO: Run next board state

            _boardContext.GameBoards.Update(gameBoard);
            await _boardContext.SaveChangesAsync();

            return gameBoard;
        }

        public async Task<GameBoard> GetBoardFinalState(int boardId, int numberOfAttempts)
        {
            var gameBoard = await _boardContext.GameBoards.FindAsync(boardId);

            if (gameBoard == null)
            {
                return null;
            }

            //TODO: Run final board state

            _boardContext.GameBoards.Update(gameBoard);
            await _boardContext.SaveChangesAsync();

            return gameBoard;
        }
    }
}
