using GameOfLife.Context;
using GameOfLife.Controllers;
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
    }
}
