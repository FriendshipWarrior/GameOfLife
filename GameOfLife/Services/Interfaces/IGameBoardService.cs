using GameOfLife.Domain;

namespace GameOfLife.Services.Interfaces
{
    public interface IGameBoardService
    {
        Task<int> CreateGameBoard(CreateBoardRequest request);
        Task<GameBoard> GetBoardNextState(int boardId, int numberOfStates);
        Task<GameBoard> GetBoardFinalState(int boardId, int numberOfAttempts);
    }
}
